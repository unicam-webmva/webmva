#!/bin/bash
 
########################
## INSTALLAZIONE ODAT ##
########################
 
echo "Verrà richiesta la password per sudo."
curDir=$PWD
cd Programmi
git clone https://github.com/quentinhardy/odat.git
cd odat
git submodule init
git submodule update

cd $curDir/Script/Odat

if ! hash alien >/dev/null 2>&1 ; then 
sudo apt-get install alien -y > /dev/null ; fi 

if [ ! -f /lib/x86_64-linux-gnu/libaio.so.1 ] ; then
sudo apt-get install libaio1 -y > /dev/null ; fi

sudo alien --to-deb oracle-instantclient12.2-basic-12.2.0.1.0-1.x86_64.rpm
sudo alien --to-deb oracle-instantclient12.2-sqlplus-12.2.0.1.0-1.x86_64.rpm
sudo alien --to-deb oracle-instantclient12.2-devel-12.2.0.1.0-1.x86_64.rpm

sudo dpkg -i oracle-instantclient12.2-basic-12.2.0.1.0-1.x86_64.deb
sudo dpkg -i oracle-instantclient12.2-sqlplus-12.2.0.1.0-1.x86_64.deb
sudo dpkg -i oracle-instantclient12.2-devel-12.2.0.1.0-1.x86_64.deb

echo "export LD_LIBRARY_PATH=/opt/oracle/instantclient_12_2:$LD_LIBRARY_PATH" | sudo tee -a /etc/profile
echo "export PATH=/usr/lib/oracle/12.2/client64/bin:$PATH" | sudo tee -a /etc/profile

sudo ln -s /usr/lib/oracle/12.2/client64/lib/libclntsh.so.11.1   /usr/lib/oracle/12.2/client64/lib/libclntsh.so
sudo touch /etc/ld.so.conf.d/oracle.conf 
echo "/usr/lib/oracle/12.2/client64/lib/" | sudo tee -a /etc/ld.so.conf.d/oracle.conf

sudo ldconfig

sudo -s
source /etc/profile
pip install cx_Oracle

if ! hash scapy >/dev/null 2>&1 ; then 
sudo apt-get install python-scapy -y > /dev/null ; fi 

sudo pip install colorlog termcolor pycrypto passlib > /dev/null
sudo pip install argcomplete && sudo activate-global-python-argcomplete > /dev/null

exit 0