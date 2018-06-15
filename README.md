# WebMVA


## Realizzatori del progetto


*  Riccardo Cannella, 096001, riccardo.cannella@studenti.unicam.it 
*  Margherita Renieri, 095100, margherita.renieri@studenti.unicam.it 

## Ambito

Penetration Testing Tools


### Progetto

Realizzazione di una pagina web per la gestione locale di test di vulnerabilità del sistema e la generazione di report di risultati.
La piattaforma è progettata per supportare l'automazione in ogni fase del processo e consentire la personalizzazione per qualsiasi altro sistema che si utilizza.


### Obiettivi

**WebMVA** si propone di rendere il processo di pentesting il più semplice possibile e creare report di ricerca di facile comprensione e utilizzabili dal cliente finale. 
Il sistema esegue la scansione e rileva le vulnerabilità di rete prima che siano esposte, riducendo il tempo necessario per applicare le patch sul sistema del cliente.
I dati immessi nel sistema mantengono una forma ben strutturata e organizzata per facilitare le eventuali ricerche nello storico di ciascun cliente. 


### Tecnologie usate

ASP.NET Core, Entity Framework, HTML, Javascript, Bash Scripting, XSLT


## Dipendenze


WebMVA dipende da una serie di pacchetti:
* [.NET Core](https://www.microsoft.com/net/)
* [nmap](https://nmap.org/)
* [ruby](https://www.ruby-lang.org/it/)
* [perl](https://www.perl.org/)
* [Apache fop](https://xmlgraphics.apache.org/fop/)
* [wkhtmltopdf](https://wkhtmltopdf.org/)
* [Python](https://www.python.org/) (sia 2 che 3)
* [pip](https://pypi.org/project/pip/) (sia 2 che 3)
* [timedatectl](https://www.freedesktop.org/software/systemd/man/timedatectl.html) (per sincronizzazione tempo con NTP)
* [aircrack-ng](https://www.aircrack-ng.org/), [cowpatty](https://github.com/joswr1ght/cowpatty), [bully](https://github.com/aanarchyy/bully), [reaver](https://code.google.com/archive/p/reaver-wps/), [pyrit](https://github.com/JPaulMora/Pyrit) e [tshark](https://www.wireshark.org/docs/man-pages/tshark.html) per Wifite2

Inoltre la cartella ```Programmi``` contiene diversi software:
* [dnsenum](https://github.com/fwaeytens/dnsenum)
* [dnsrecon](https://github.com/darkoperator/dnsrecon)
* [droopescan](https://github.com/droope/droopescan)
* [fierce](https://github.com/mschwager/fierce)
* [Infoga](https://github.com/m4ll0k/Infoga)
* [joomscan](https://github.com/rezasp/joomscan)
* [NoSQLMap](https://github.com/codingo/NoSQLMap)
* [ODAT](https://github.com/quentinhardy/odat)
* [OpenDoor](https://github.com/stanislav-web/OpenDoor)
* [SQLMap](https://github.com/sqlmapproject/sqlmap)
* [Sublist3r](https://github.com/aboul3la/Sublist3r)
* [Wapiti](http://wapiti.sourceforge.net/)
* [WAScan](https://github.com/m4ll0k/WAScan)
* [Wifite2](https://github.com/derv82/wifite2)
* [WPScan](https://wpscan.org/)
****
## Installazione automatizzata
```
$ git clone https://github.com/unicam-webmva/webmva
$ cd webmva
$ ./install.sh
```
### Avvio
```
sudo dotnet run
```
## Installazione manuale

Per iniziare: 
```
git clone https://github.com/unicam-webmva/webmva
cd webmva
WEBMVADIR=$PWD
```
### Inizializzazione sottomoduli

I sottomoduli che sono nella cartella `Programmi` vanno inizializzati tramite i comandi:
```
git submodule init
git submodule update
```
In questo modo tutti i software (con l'unica eccezione di Wapiti) saranno scaricati automaticamente all'ultima versione disponibile.

### Installazione dipendenze

Per installare tutte le dipendenze necessarie dai repository di Ubuntu, eseguire:
```
sudo apt-get install build-essential nmap fop wkhtmltopdf python2.7 python3 python-pip python3-pip timedatectl aircrack-ng reaver pyrit
```
Per sincronizzare l'ora del PC usando NTP:
```
timedatectl set-ntp true
```
Per quanto riguarda .NET Core:
```
wget -qO- https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.asc.gpg
mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/
wget -q https://packages.microsoft.com/config/ubuntu/18.04/prod.list
mv prod.list /etc/apt/sources.list.d/microsoft-prod.list
apt-get install apt-transport-https
apt-get update
apt-get install dotnet-sdk-2.1.200
```

Per quanto riguarda le dipendenze installabili con pip:
```
pip install -r ${WEBMVADIR}/Script/requirements.txt
pip3 install -r ${WEBMVADIR}/Script/requirements.txt
activate-global-argcomplete
```

Per quanto riguarda ruby e WPScan:
```
sudo apt-get install ruby ruby-dev ruby-all-dev libffi-dev build-essential patch zlib1g-dev liblzma-dev
```
Per quanto riguarda perl:
```
sudo apt-get install perl cpanminus libnet-whois-ip-perl
cpanm String::Random Net::IP Net::DNS Net::Netmask HTML::Parser WWW::Mechanize XML::Writer
```
Per quanto riguarda ODAT:
```
sudo apt-get install alien libaio1 python-scapy
```

### Repository di Kali Linux

Per poter installare Bully e coWPAtty è necessario avere nelle liste di apt i repository di Kali Linux:
```
echo "deb http://http.kali.org/kali kali-rolling main non-free contrib" | sudo tee -a /etc/apt/sources.list
sudo apt-key adv --keyserver hkp://keys.gnupg.net --recv-keys ED444FF07D8D0BF6
sudo apt-get update
sudo apt-get install bully cowpatty
```
Eventualmente è possibile disattivare il repository appena inserito per non creare disagi durante gli upgrade di sistema (perdendo però l'abilità di aggiornare Bully e coWPAtty):
```
sudo sed -i 's/^deb http://http.kali.org/#deb http://http.kali.org' /etc/apt/sources.list
sudo apt-get update
```

### Configurazione WPScan

WPScan necessita di alcuni passaggi supplementari:
```
cd ${WEBMVADIR}/Programmi/wpscan
sudo gem install bundler && bundle install
unzip data.zip -d ${HOME}/.wpscan/
cd ${WEBMVADIR}
```

### Configurazione ODAT

ODAT necessita di alcuni passaggi supplementari:
```
cd ${WEBMVADIR}/Programmi/odat
git submodule init
git submodule update
sudo alien --to-deb ${WEBMVADIR}/Script/Odat/oracle-instantclient12.2-basic-12.2.0.1.0-1.x86_64.rpm
sudo dpkg -i ${WEBMVADIR}/Script/Odat/oracle-instantclient12.2-basic-12.2.0.1.0-2.x86_64.deb
sudo alien --to-deb ${WEBMVADIR}/Script/Odat/oracle-instantclient12.2-sqlplus-12.2.0.1.0-1.x86_64.rpm
sudo dpkg -i ${WEBMVADIR}/Script/Odat/oracle-instantclient12.2-basic-12.2.0.1.0-2.x86_64.rpm
sudo alien --to-deb ${WEBMVADIR}/Script/Odat/oracle-instantclient12.2-devel-12.2.0.1.0-1.x86_64.rpm
sudo dpkg -i ${WEBMVADIR}/Script/Odat/oracle-instantclient12.2-basic-12.2.0.1.0-2.x86_64.rpm
echo "export LD_LIBRARY_PATH=/opt/oracle/instantclient_12_2:\$LD_LIBRARY" | sudo tee -a /etc/profile
echo "export PATH=/usr/lib/oracle/12.2/client64/bin:\$PATH | sudo tee -a /etc/profile
sudo ln -s /usr/lib/oracle/12.2/client64/lib/libclntsh.so.11.1   /usr/lib/oracle/12.2/client64/lib/libclntsh.so
sudo touch /etc/ld.so.conf.d/oracle.conf 
echo "/usr/lib/oracle/12.2/client64/lib/" | sudo tee -a /etc/ld.so.conf.d/oracle.conf
sudo ldconfig
source /etc/profile
cd ${WEBMVADIR}
```

### Installazione OpenDoor

OpenDoor va installato e configurato:
```
git clone https://github.com/stanislav-web/OpenDoor.git ${wEBMVADIR}/OpenDoor
cd ${WEBMVADIR}/OpenDoor
sudo su 
python3 setup.py build && python3 setup.py install
cp -R data/ /usr/local/bin/data
exit
cd ${WEBMVA}
rm -rf ${WEBMVA}/OpenDoor/
```