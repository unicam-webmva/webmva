#!/bin/bash
if [[ $EUID > 0 ]] ;
  then 
  echo "Concedere i permessi root."
  exec sudo /bin/bash "$0" "$@ $HOME"
  exit
fi
WORKINGDIR=$PWD
MYHOME=$1
if [[ $MYHOME = "" ]] ;
then
  echo "Bisogna passare come parametro la home dell'utente."
  echo "Premere un tasto per chiudere questo script..."
  read
  exit
fi



echo "-------------------------------------------------------"
echo "Installazione dipendenze WEBMVA"
echo "-------------------------------------------------------"
echo "Autori: Margherita Renieri, Riccardo Cannella"
echo "-------------------------------------------------------"
echo " "
echo " "
echo "Sto eseguendo apt-get update..."
apt-get update > /dev/null
echo "-------------------------------------------------------"
echo "Inizializzazione dei sottomoduli di WebMVA"
echo "-------------------------------------------------------"
echo "Sto scaricando i sottomoduli necessari al funzionamento di WebMVA..."
git submodule init && git submodule update >/dev/null
echo "Inizializzazione dei sottomoduli terminata."
echo "-------------------------------------------------------"
echo "INSTALLAZIONE WKHTMLTOPDF"
echo "-------------------------------------------------------"

if hash wkhtmltopdf >/dev/null 2>&1 ; 
then
	echo "wkhtmltopdf è già installato.";
else {
	echo "Sto installando wkhtmltopdf..."
	apt-get install xvfb libfontconfig wkhtmltopdf -y >/dev/null
	echo "Fine installazione wkhtmltopdf."
	}
fi
echo "-------------------------------------------------------"
echo "INSTALLAZIONE NMAP"
echo "-------------------------------------------------------"

if hash nmap >/dev/null 2>&1 ; 
then
	echo "nmap è già installato.";
else {
	echo "Sto installando nmap..."
	apt-get install nmap -y > /dev/null
	echo "Fine installazione NMAP."
	}
fi
echo "-------------------------------------------------------"
echo "INSTALLAZIONE RUBY E PERL"
echo "-------------------------------------------------------"

if hash ruby >/dev/null 2>&1 ; 
then
	echo "ruby è già installato.";
else {
	echo "Sto installando ruby..."
	apt-get install ruby -y > /dev/null
	echo "Fine installazione ruby."
	}
fi
if hash perl >/dev/null 2>&1 ; 
then
	echo "perl è già installato.";
else {
	echo "Sto installando perl..."
	apt-get install perl -y > /dev/null
	echo "Fine installazione perl."
	}
fi
echo "-------------------------------------------------------"
echo "INSTALLAZIONE PYTHON"
echo "-------------------------------------------------------"

if hash pip && hash pip3 >/dev/null 2>&1 ;
then echo "Sia Python che pip sono installati, sia 2.7 che 3." ;
else {
	if hash python && hash python2 >/dev/null 2>&1 ;
	then {
		echo "Python è installato ma pip no."
		echo "Sto installando pip..."
		apt-get install python2.7-dev python3-dev python-pip python3-pip -y > /dev/null
		echo "Fine installazione pip."
	}
	else {
		echo "Sto installando Python versione 2.7 e 3 e pip..."
		apt-get install python2.7 python2.7-dev python-pip python3 python3-dev python3-pip -y > /dev/null
		echo "Fine installazione Python e pip."
	};
	fi;
};
fi
if hash scapy >/dev/null 2>&1 ; then echo "Scapy è già installato." ;
else { 
	echo "Sto installando python-scapy..."
	apt-get install python-scapy >/dev/null
	echo "Fine installazione python-scapy."
}
fi
echo "-------------------------------------------------------"
echo "INSTALLAZIONE DIPENDENZE PER SCRIPT PYTHON"
echo "-------------------------------------------------------"
echo "Sto installando alcune dipendenze tramite pip e pip3..."
PIP=`pip freeze`
PIP3=`pip3 freeze`
while read p; do
  if [[ ${PIP} = *$p* ]] ; then echo "Pacchetto ${p} (pip2) già installato." ; else  pip install $p >/dev/null ; fi
  if [[ ${PIP3} = *$p* ]] ; then echo "Pacchetto ${p} (pip3) già installato." ; else  pip3 install $p >/dev/null ; fi
done <${WORKINGDIR}/Script/requirements.txt
activate-global-argcomplete

echo "Fine installazione dipendenze script PYTHON."
echo "-------------------------------------------------------"
echo "INSTALLAZIONE DIPENDENZE PER WIFITE2"
echo "-------------------------------------------------------"

if hash bully && hash reaver && hash tshark && hash aircrack-ng && hash cowpatty && hash pyrit >/dev/null 2>&1 ;
then echo "Tutte le dipendenze di Wifite2 sono già installate.";
else {
	if grep -q "kali-rolling" /etc/apt/sources.list ;
	then 
		echo "I repository di Kali sono già in questo sistema, non li aggiungo."
		echo "Sto installando cowpatty e bully dai repo di kali..."
		apt-get install bully cowpatty -y >/dev/null ;
	else {
		echo "Verranno aggiunti i repository di Kali Linux per installare alcuni programmi non presenti in quelli di Ubuntu..."
		echo "deb http://http.kali.org/kali kali-rolling main non-free contrib" | tee -a /etc/apt/sources.list > /dev/null
		apt-key adv --keyserver hkp://keys.gnupg.net --recv-keys ED444FF07D8D0BF6  > /dev/null
		apt-get update > /dev/null
		echo "Sto installando cowpatty e bully dai repo di kali..."
		apt-get install bully cowpatty -y >/dev/null
		echo "Verranno tolti i repo di kali per non interferire nel sistema in uso."
		sed -i 's/^deb http://http.kali.org/#deb http://http.kali.org' /etc/apt/sources.list
	};
	fi
	echo "Sto installando aircrack-ng, reaver, pyrit e tshark tramite i repository normali..."
	apt-get install aircrack-ng reaver pyrit -y > /dev/null
	DEBIAN_FRONTEND=noninteractive apt-get -y install tshark > /dev/null
	echo "Fine installazione dipendenze per Wifite2."
};
fi
echo "-------------------------------------------------------"
echo "INSTALLAZIONE .NET CORE 2 SDK"
echo "-------------------------------------------------------"

if hash dotnet >/dev/null 2>&1 ;
then echo ".NET Core 2 SDK è già installato." ;
else {
	echo "Sto installando il kit di sviluppo di .NET Core 2..."
	wget -qO- https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.asc.gpg  > /dev/null
	mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/ > /dev/null
	wget -q https://packages.microsoft.com/config/ubuntu/18.04/prod.list > /dev/null
	mv prod.list /etc/apt/sources.list.d/microsoft-prod.list > /dev/null
	apt-get install apt-transport-https > /dev/null
	apt-get update > /dev/null
	apt-get install -y dotnet-sdk-2.1.200 > /dev/null
	echo "Fine installazione DOTNET SDK."
};
fi
echo "-------------------------------------------------------"
echo "INSTALLAZIONE DIPENDENZE DNSENUM"
echo "-------------------------------------------------------"
if hash cpanm >/dev/null 2>&1 ;
then echo "cpanm è già installato, procedo con l'installazione delle dipendenze..." ;
else {
	echo "Sto installando cpanm..."
	apt-get install cpanminus -y > /dev/null
	echo "Fine installazione cpanm, procedo con l'installazione delle dipendenze..."
};
fi

perl -MNet::Whois::IP -e 1 && if [[ ! $? = 0 ]] ; then  apt install libnet-whois-ip-perl >/dev/null ; else echo "Pacchetto Net:Whois:IP già installato." ; fi

while read p; do
  perl -M$p -e 1 && if [[ ! $? = 0 ]] ; then  cpanm $p >/dev/null ; else echo "Pacchetto ${p} già installato." ; fi
done <${WORKINGDIR}/Script/dipendenzePerl.txt

echo "Fine installazione dipendenze di DnsEnum."
echo "-------------------------------------------------------"
echo "INSTALLAZIONE WPScan"
echo "-------------------------------------------------------"
cd ${WORKINGDIR}/Programmi/wpscan
if ! hash bundle >/dev/null 2>&1 ; then 
apt install ruby-all-dev ruby-dev libffi-dev build-essential patch ruby-dev zlib1g-dev liblzma-dev -y > /dev/null ;
fi
bundle check --gemfile=Gemfile >/dev/null ;
if [[ ! $0 = 0 ]] ; then 
sudo gem install bundler && bundle install --without test ;
fi
if [ ! -d ~/.wpscan/data ] >/dev/null 2>&1 ; then 
unzip data.zip -d ~/.wpscan/ ; fi
if [ ! -d "${MYHOME}/.wpscan/data" ] >/dev/null 2>&1 ; then 
unzip data.zip -d ${MYHOME}/.wpscan/ ; fi
cd ${WORKINGDIR}
echo "Fine installazione WPScan."
echo "-------------------------------------------------------"
echo "INSTALLAZIONE FOP"
echo "-------------------------------------------------------"

if hash fop >/dev/null 2>&1 ; 
then
	echo "fop è già installato.";
else {
	echo "Sto installando fop..."
	apt-get install fop >/dev/null
	echo "Fine installazione fop."
	}
fi
echo "-------------------------------------------------------"
echo "INSTALLAZIONE OPENDOOR"
echo "-------------------------------------------------------"

if hash opendoor >/dev/null 2>&1 ; 
then echo "opendoor è già installato." ;
else {
	echo "Sto installando opendoor..."
	git clone https://github.com/stanislav-web/OpenDoor.git ${WORKINGDIR}/OpenDoor
 	cd ${WORKINGDIR}/OpenDoor
 	sudo su 
	python3 setup.py build && python3 setup.py install
	cp -R data/ /usr/local/bin/data
	exit
	cd ${WORKINGDIR}
	rm -rf ${WORKINGDIR}/OpenDoor/
	echo "Fine installazione opendoor."
	}
fi
echo "-------------------------------------------------------"
echo "INSTALLAZIONE TIMEDATECTL"
echo "-------------------------------------------------------"

if hash timedatectl >/dev/null 2>&1 ; 
then
	echo "timedatectl è già installato."
	systemctl is-active --quiet systemd-timesyncd.service && if [[ ! $? = 0 ]] ; then timedatectl set-ntp true ; fi
	echo "timedatectl è attivo." ;
else {
	echo "Sto installando e attivando timedatectl..."
	apt install timedatectl >/dev/null
	timedatectl set-ntp true
	echo "Fine installazione timedatectl." ;
}
fi
echo "-------------------------------------------------------"
echo "INSTALLAZIONE ODAT"
echo "-------------------------------------------------------"
bash ${WORKINGDIR}/Script/Odat/installOdat.sh ;
echo "Fine installazione Odat."
echo ""
echo ""
echo ""
echo "-------------------------------------------------------"
echo "FINE INSTALLAZIONE DIPENDENZE"
echo "-------------------------------------------------------"
echo "Tutte le dipendenze di WebMVA sono ora presenti nel sistema."
echo "Per iniziare ad usare WebMVA, eseguire:"
echo "\tsudo dotnet run"
exit 0