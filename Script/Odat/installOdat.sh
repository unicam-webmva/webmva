#!/bin/bash
 
########################
## INSTALLAZIONE ODAT ##
########################
if [[ $EUID > 0 ]] ;
  then 
  echo "Bisogna farmi partire come root."
  echo "Premere un tasto per chiudere questo script..."
  read
  exit
fi

curDir=$PWD
cd Programmi
if [ ! -d odat ] ; then
git clone https://github.com/quentinhardy/odat.git 
fi
cd odat
git submodule init
git submodule update

cd $curDir/Script/Odat

if ! hash alien >/dev/null 2>&1 ; then 
apt-get install alien -y > /dev/null ; fi 

if [ ! -f /lib/x86_64-linux-gnu/libaio.so.1 ] ; then
apt-get install libaio1 -y > /dev/null ; fi

if [ ! -f oracle-instantclient12.2-basic_12.2.0.1.0-2_amd64.deb ] ; then
alien --to-deb oracle-instantclient12.2-basic-12.2.0.1.0-1.x86_64.rpm ;
fi
if [ ! -f oracle-instantclient12.2-sqlplus_12.2.0.1.0-2_amd64.deb ] ; then
alien --to-deb oracle-instantclient12.2-sqlplus-12.2.0.1.0-1.x86_64.rpm
fi
if [ ! -f oracle-instantclient12.2-devel_12.2.0.1.0-2_amd64.deb ] ; then
alien --to-deb oracle-instantclient12.2-devel-12.2.0.1.0-1.x86_64.rpm
fi
if [ ! -f /usr/lib/oracle/12.2/client64/bin/genezi ] ; then
dpkg -i oracle-instantclient12.2-basic_12.2.0.1.0-2_amd64.deb ;
fi
if [ ! -f /usr/lib/oracle/12.2/client64/bin/sqlplus ] ; then 
dpkg -i oracle-instantclient12.2-sqlplus_12.2.0.1.0-2_amd64.deb ; 
fi
if [ ! -d /usr/lib/oracle/12.2/client64/lib ] ; then
dpkg -i oracle-instantclient12.2-devel_12.2.0.1.0-2_amd64.deb ;
fi
if ! grep -q "LD_LIBRARY_PATH" /etc/profile ; then
echo "export LD_LIBRARY_PATH=/opt/oracle/instantclient_12_2:\$LD_LIBRARY_PATH" | tee -a /etc/profile
echo "export PATH=/usr/lib/oracle/12.2/client64/bin:\$PATH" | tee -a /etc/profile ;
fi
echo "fase ln"
ln -s /usr/lib/oracle/12.2/client64/lib/libclntsh.so.11.1   /usr/lib/oracle/12.2/client64/lib/libclntsh.so
echo "fase conf"
if [ ! -f /etc/ld.so.conf.d/oracle.conf  ] ; then
touch /etc/ld.so.conf.d/oracle.conf 
echo "/usr/lib/oracle/12.2/client64/lib/" | tee -a /etc/ld.so.conf.d/oracle.conf 

ldconfig ;
fi
echo "fase pip"
source /etc/profile

if ! hash scapy >/dev/null 2>&1 ; then 
apt-get install python-scapy -y > /dev/null ; fi 
while read p; do
  pip freeze | grep -q $p && if [[ ! $? = 0 ]] ; then  pip install $p >/dev/null ; else echo "Pacchetto ${p} (pip2) già installato." ; fi
done <requirements.txt
pip freeze | grep -q argcomplete && if [[ ! $? = 0 ]] ; then  pip install argcomplete && activate-global-python-argcomplete >/dev/null ; else echo "Pacchetto ${p} (pip2) già installato." ; fi

exit 0

7x13
9x15