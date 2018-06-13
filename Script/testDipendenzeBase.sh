#!/bin/bash

echo "######################"
echo "## CHECK DIPENDENZE ##"
echo "######################"

###########
# Exit codes:
# 1: manca qualcosa
# 0: tutto presente
###########

MANCANTI=""
if ! hash nmap >/dev/null 2>&1 ; then MANCANTI="${MANCANTI}nmap " ; fi
if ! hash python2 >/dev/null 2>&1 ; then MANCANTI="${MANCANTI}python2 " ; fi
if ! hash python3 >/dev/null 2>&1 ; then MANCANTI="${MANCANTI}python3 " ; fi
if ! hash pip >/dev/null 2>&1 ; then MANCANTI="${MANCANTI}pip2 " ; fi
if ! hash pip3 >/dev/null 2>&1 ; then MANCANTI="${MANCANTI}pip3 " ; fi
if ! hash aircrack-ng >/dev/null 2>&1 ; then MANCANTI="${MANCANTI}aircrack " ; fi
if ! hash reaver >/dev/null 2>&1 ; then MANCANTI="${MANCANTI}reaver " ; fi
if ! hash cowpatty >/dev/null 2>&1 ; then MANCANTI="${MANCANTI}cowpatty " ; fi
if ! hash bully >/dev/null 2>&1 ; then MANCANTI="${MANCANTI}bully " ; fi
if ! hash tshark >/dev/null 2>&1 ; then MANCANTI="${MANCANTI}tshark " ; fi
if ! hash pyrit >/dev/null 2>&1 ; then MANCANTI="${MANCANTI}pyrit " ; fi
if ! hash dotnet >/dev/null 2>&1 ; then MANCANTI="${MANCANTI}dotnet " ; fi
if ! hash perl >/dev/null 2>&1 ; then MANCANTI="${MANCANTI}perl " ; fi
if ! hash ruby >/dev/null 2>&1 ; then MANCANTI="${MANCANTI}ruby " ; fi
if [ ! -d Programmi/odat ] ; then MANCANTI="${MANCANTI}odat " ; fi
if ! hash fop >/dev/null 2>&1 ; then MANCANTI="${MANCANTI}fop " ; fi
#questo mega if è tutto per wpscan
if ! hash bundle >/dev/null 2>&1 ; 
    then  MANCANTI="${MANCANTI}wpscan " ;
else  {
    bundle check --gemfile=/home/rick/webmva/Programmi/wpscan/Gemfile >/dev/null
    CODICEUSCITA=$?
    if [[ $CODICEUSCITA -gt 0 ]] ;
        then MANCANTI="${MANCANTI}wpscan " ;
    fi ; 
    } 
fi

if ! hash opendoor >/dev/null 2>&1 ; then MANCANTI="${MANCANTI}opendoor " ; fi

if ! hash wkhtmltopdf >/dev/null 2>&1 ; then MANCANTI="${MANCANTI}wkhtmltopdf " ; fi

EXIT=0
if [[ "${MANCANTI}" = "" ]] 
    then EXIT=1 
else EXIT=0  
fi
echo $MANCANTI
exit $EXIT