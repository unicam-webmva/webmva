#!/bin/bash

#if [[ $EUID > 0 ]] ;
#  then 
#  echo "Concedere i permessi root."
#  exec sudo /bin/bash "$0" "$@"
#  exit
#fi
WORKINGDIR=$PWD
MYHOME=$1
#if [[ $MYHOME = "" ]] ;
#then
#  echo "Bisogna passare come parametro la home dell'utente."
#  echo "Premere un tasto per chiudere questo script..."
#  read
#  exit
#fi



echo "-------------------------------------------------------"
echo "Installazione dipendenze WEBMVA"
echo "-------------------------------------------------------"
echo "Autori: Margherita Renieri, Riccardo Cannella"
echo "-------------------------------------------------------"
echo " "
echo " "
echo "Sto eseguendo apt-get update..."
sudo apt-get update > /dev/null
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
	sudo apt-get install xvfb libfontconfig wkhtmltopdf -y >/dev/null
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
	sudo apt-get install nmap -y > /dev/null
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
	sudo apt-get install ruby -y > /dev/null
	echo "Fine installazione ruby."
	}
fi
if hash perl >/dev/null 2>&1 ; 
then
	echo "perl è già installato.";
else {
	echo "Sto installando perl..."
	sudo apt-get install perl -y > /dev/null
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
		sudo apt-get install python2.7-dev python3-dev python-pip python3-pip -y > /dev/null
		echo "Fine installazione pip."
	}
	else {
		echo "Sto installando Python versione 2.7 e 3 e pip..."
		sudo apt-get install python2.7 python2.7-dev python-pip python3 python3-dev python3-pip -y > /dev/null
		echo "Fine installazione Python e pip."
	};
	fi;
};
fi
if hash scapy >/dev/null 2>&1 ; then echo "Scapy è già installato." ;
else { 
	echo "Sto installando python-scapy..."
	sudo apt-get install python-scapy >/dev/null
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
activate-global-python-argcomplete

echo "Fine installazione dipendenze script PYTHON."
echo "-------------------------------------------------------"
echo "INSTALLAZIONE DIPENDENZE PER WIFITE2"
echo "-------------------------------------------------------"

if hash reaver >/dev/null 2>&1 && hash tshark >/dev/null 2>&1 && hash aircrack-ng >/dev/null 2>&1 && hash pyrit >/dev/null 2>&1 ;
then echo "Tutte le dipendenze di Wifite2 sono già installate.";
else {
	echo "Installazione di reaver, tshark, aircrack-ng e pyrit..."
	sudo apt-get install -y reaver aircrack-ng pyrit >/dev/null
	DEBIAN_FRONTEND=noninteractive sudo apt-get -y install tshark > /dev/null
	echo "Fine installazione dipendenze di Wifite2."
}
fi
echo "-------------------------------------------------------"
echo "INSTALLAZIONE DAI REPO DI KALI"
echo "-------------------------------------------------------"
if hash bully >/dev/null 2>&1 && hash cowpatty >/dev/null 2>&1 && hash theharvester >/dev/null 2>&1 ;
then echo "Tutte le dipendenze di Wifite2 sono già installate.";
else {
	TROVATO=false
	while IFS=$'\n' read line ; do  
    	if [[ "$line" =~ \#.* ]] ; then
        	continue
    	else
		if [[ "$line" = *"kali"* ]] ; then
        		TROVATO=true ;
		fi
    	fi
	done < /etc/apt/sources.list
if [[ $TROVATO = "true" ]] ;
	then 
		echo "I repository di Kali sono già in questo sistema, non li aggiungo."
		echo "Sto installando theharvester, cowpatty e bully dai repo di kali..."
		sudo apt-get install theharvester bully cowpatty -y >/dev/null ;
	else {
		echo "Verranno aggiunti i repository di Kali Linux per installare alcuni programmi non presenti in quelli di Ubuntu..."
		echo "deb http://http.kali.org/kali kali-rolling main non-free contrib" | sudo tee -a /etc/apt/sources.list > /dev/null
		sudo apt-key adv --keyserver hkp://keys.gnupg.net:80 --recv-keys ED444FF07D8D0BF6 
		sudo apt-get update > /dev/null
		echo "Sto installando theharvester, cowpatty e bully dai repo di kali..."
		sudo apt-get install theharvester bully cowpatty -y >/dev/null
		echo "Verranno tolti i repo di kali per non interferire nel sistema in uso."
		sudo sed -i '/kali-rolling main non-free contrib/d' /etc/apt/sources.list
	};
	fi
	echo "Fine installazione pacchetti dai repo di Kali."
};
fi
echo "-------------------------------------------------------"
echo "INSTALLAZIONE .NET CORE 2 SDK"
echo "-------------------------------------------------------"

if hash dotnet >/dev/null 2>&1 ;
then echo ".NET Core 2 SDK è già installato." ;
else {
	echo "Sto installando il kit di sviluppo di .NET Core 2..."
	wget -qO- https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.asc.gpg
	sudo mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/
	wget -q https://packages.microsoft.com/config/ubuntu/18.04/prod.list
	sudo mv prod.list /etc/apt/sources.list.d/microsoft-prod.list
	sudo chown root:root /etc/apt/trusted.gpg.d/microsoft.asc.gpg
	sudo chown root:root /etc/apt/sources.list.d/microsoft-prod.list
	sudo apt-get install -y apt-transport-https > /dev/null
	sudo apt-get update > /dev/null
	sudo apt-get install -y dotnet-sdk-2.1 > /dev/null
	echo "Fine installazione DOTNET SDK."
};
fi
echo "-------------------------------------------------------"
echo "INSTALLAZIONE DIPENDENZE DNSENUM"
echo "-------------------------------------------------------"
echo "Sto installando le dipendenze di DnsEnum..."
sudo apt-get install libnet-whois-ip-perl libnet-ip-perl libnet-dns-perl libnet-netmask-perl libhtml-parser-perl libwww-mechanize-perl libxml-writer-perl libstring-random-perl -y >/dev/null

echo "Fine installazione dipendenze di DnsEnum."
echo "-------------------------------------------------------"
echo "INSTALLAZIONE WHOIS"
echo "-------------------------------------------------------"
echo "Sto installando whois..."
if hash whois >/dev/null 2>&1 ; 
then
	echo "whois è già installato.";
else {
	echo "Sto installando whois..."
	sudo apt-get install whois -y > /dev/null
	echo "Fine installazione whois."
	}
fi

echo "Fine installazione whois."
echo "-------------------------------------------------------"
echo "INSTALLAZIONE WPScan"
echo "-------------------------------------------------------"
cd ${WORKINGDIR}/Programmi/wpscan
if ! hash bundle >/dev/null 2>&1 ; then 
sudo apt-get install ruby-all-dev ruby-dev libffi-dev build-essential patch ruby-dev zlib1g-dev liblzma-dev -y > /dev/null ;
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
	sudo apt-get install -y fop >/dev/null
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
 	sudo python3 setup.py build && sudo python3 setup.py install
	sudo cp -R data/ /usr/local/bin/data
	cd ${WORKINGDIR}
	sudo rm -rf ${WORKINGDIR}/OpenDoor/
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
	sudo apt-get install timedatectl >/dev/null
	timedatectl set-ntp true
	echo "Fine installazione timedatectl." ;
}
fi
echo "-------------------------------------------------------"
echo "INSTALLAZIONE AMASS"
echo "-------------------------------------------------------"

if hash amass >/dev/null 2>&1 ; then 
	echo "amass è già installato"; 
else {
	if hash snap >/dev/null 2>&1 ; then 
		echo "Sto installando amass..."; 
		sudo snap install amass >/dev/null ;
		echo "Fine installazione amass." ;
	else { 
		echo "Sto installando snap...";
		sudo apt install snap >/dev/null;
		echo "Fine installazione snap, installo amass...";
		sudo snap install amass >/dev/null;
		echo "Fine installazione amass." ; 
	}; 
	fi 
}; 
fi
echo "-------------------------------------------------------"
echo "INSTALLAZIONE Drupwn"
echo "-------------------------------------------------------"

if hash drupwn >/dev/null 2>&1 ; then 
	echo "drupwn è già installato"; 
else {
	echo "Sto  installando drupwn..."
	git clone https://github.com/immunIT/drupwn ${WORKINGDIR}/drupwn
	cd ${WORKINGDIR}/drupwn
	sudo python3 setup.py install
	cd ${WORKINGDIR}
	sudo rm -rf ${WORKINGDIR}/drupwn
	echo "Fine installazione drupwn."
}; 
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
echo "./run.sh"
exit 0