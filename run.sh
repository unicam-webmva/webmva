#!/bin/bash

WORKINGDIR=$PWD
echo "-------------------------------------------------------"
echo "                      WEBMVA"
echo "              Collaborative tool for"
echo "          Vulnerability assessment analysis"
echo "-------------------------------------------------------"
echo "Autori: Margherita Renieri, Riccardo Cannella"
echo "-------------------------------------------------------"
echo " "
echo " "
echo "Questo è un software open source sotto licenza MIT."
echo "Una copia dei termini della licenza è disponibile"
echo "nel file LICENSE."
echo " "
echo " "
echo " "
echo " "
if [ ! -f ${PWD}/webmva.csproj ] ; then echo "Impossibile avviare WebMVA: cartella errata.";  exit ; fi
echo "Controllo aggiornamenti..."
git pull >/dev/null
echo "Avvio di WebMVA in corso..."
dotnet run