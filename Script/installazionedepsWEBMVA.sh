#!/bin/bash
if [[ $EUID > 0 ]]
  then echo "Bisogna farmi partire come root."
  exit
fi


echo "-------------------------------------------------------"
echo "Installazione dipendenze WEBMVA"
echo "-------------------------------------------------------"
echo "Autori: Margherita Renieri, Riccardo Cannella"
echo "Ultima modifica: 29 maggio 2018"
echo "-------------------------------------------------------"
echo " "
echo " "

apt-get update > /dev/null

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
echo " "
echo " "
echo " "
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
echo " "
echo " "
echo " "

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
echo " "
echo " "
echo " "

echo "-------------------------------------------------------"
echo "INSTALLAZIONE DIPENDENZE PER SCRIPT PYTHON"
echo "-------------------------------------------------------"
echo "Sto installando alcune dipendenze tramite pip e pip3 per i vari programmi usati all'interno di WEBMVA..."

pip install tabulate json2html urllib3 requests beautifulsoup4 yaswfp tld html5lib cement pystache futures dnspython netaddr lxml argparse mako pysocks requests_ntlm requests_kerberos > /dev/null
pip3 install tabulate json2html urllib3 requests beautifulsoup4 yaswfp tld html5lib cement pystache futures dnspython netaddr lxml argparse mako pysocks requests_ntlm requests_kerberos > /dev/null

echo "Fine installazione dipendenze script PYTHON."
echo " "
echo " "
echo " "

echo "-------------------------------------------------------"
echo "INSTALLAZIONE DIPENDENZE PER WIFITE2"
echo "-------------------------------------------------------"

if hash bully && hash reaver && hash tshark && hash aircrack-ng && hash cowpatty >/dev/null 2>&1 ;
then echo "Tutte le dipendenze di Wifite2 sono già installate.";
else {
	if grep -q "kali-rolling" /etc/apt/sources.list ;
	then echo "I repository di Kali sono già in questo sistema, non li aggiungo.";
	else {
		echo "Aggiungo i repository di Kali Linux per installare alcuni programmi non presenti in quelli di Ubuntu..."
		echo "deb http://http.kali.org/kali kali-rolling main non-free contrib" | tee -a /etc/apt/sources.list > /dev/null
		apt-key adv --keyserver hkp://keys.gnupg.net --recv-keys ED444FF07D8D0BF6  > /dev/null
		apt-get update > /dev/null
	};
	fi
	echo "Sto installando aircrack-ng, reaver, cowpatty, bully e tshark tramite i repository di Kali Linux..."
	apt-get install aircrack-ng reaver cowpatty bully pyrit -y > /dev/null
	DEBIAN_FRONTEND=noninteractive apt-get -y install tshark > /dev/null
	echo "Fine installazione dipendenze per Wifite2."
};
fi
echo " "
echo " "
echo " "

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

echo " "
echo " "
echo " "

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
cpanm String::Random Net::IP Net::DNS Net::Netmask Net::Whois::IP HTML::Parser WWW::Mechanize XML::Writer > /dev/null
echo "Fine installazione dipendenze di DnsEnum."
echo " "
echo " "
echo " "

echo "-------------------------------------------------------"
echo "INSTALLAZIONE WPScan"
echo "-------------------------------------------------------"
cd Programmi/WPScan
if ! hash bundle >/dev/null 2>&1 ; then 
apt install ruby-all-dev ruby-dev libffi-dev build-essential patch ruby-dev zlib1g-dev liblzma-dev > /dev/null ;
fi
bundle check --gemfile=/home/rick/webmva/Programmi/wpscan/Gemfile >/dev/null ;
if [[ ! $0 -eq 0 ]] ; then 
sudo gem install bundler && bundle install --without test ;
fi
if [ ! -d ~/.wpscan/data ] >/dev/null 2>&1 ; then 
unzip data.zip -d ~/.wpscan/ ; fi
cd ../..
echo "Fine installazione WPScan."
echo " "
echo " "
echo " "
echo "-------------------------------------------------------"
echo "INSTALLAZIONE WKHTMLTOPDF"
echo "-------------------------------------------------------"

if hash wkhtmltopdf >/dev/null 2>&1 ; 
then
	echo "wkhtmltopdf è già installato.";
else {
	echo "Sto installando wkhtmltopdf..."
	apt-get install xvfb libfontconfig wkhtmltopdf >/dev/null
	echo "Fine installazione wkhtmltopdf."
	}
fi
echo " "
echo " "
echo " "
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
then
	echo "opendoor è già installato.";
else {
	echo "Sto installando opendoor..."
	git clone https://github.com/stanislav-web/OpenDoor.git
 	cd OpenDoor/
 	sudo su 
	python3 setup.py build && python3 setup.py install
	cp -R data/ /usr/local/bin/data
	exit
	cd ..
	rm -rf OpenDoor/
	echo "Fine installazione opendoor."
	}
fi
echo " "
echo " "
echo " "
echo " "
echo " "
echo " "
echo "-------------------------------------------------------"
echo "INSTALLAZIONE ODAT"
echo "-------------------------------------------------------"
bash Script/Odat/installOdat.sh
echo "Fine installazione Odat."
echo " "
echo " "
echo " "

exit 0