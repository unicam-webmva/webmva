using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webmva.Models;

namespace webmva.ViewModels
{
    public class EditModuloVM
    {
        private ModuloNMAP _nMAP = null;
        private ModuloNESSUS _nESSUS = null;
        private ModuloDNSRECON _dNSRECON = null;
        private ModuloFIERCE _fIERCE = null;
        private ModuloDROOPE _dROOPE = null;
        private ModuloJOOMSCAN _jOOMSCAN = null;
        private ModuloWPSCAN _wPSCAN = null;
        private ModuloINFOGA _iNFOGA = null;
        private ModuloOPENDOOR _oPENDOOR = null;
        private ModuloINFOGAEMAIL _iNFOGAEMAIL = null;
        private ModuloSUBLIST3R _sUBLIST3R = null;
        private ModuloWASCAN _wASCAN = null;
        private ModuloWAPITI _wAPITI = null;
        private ModuloSQLMAP _sQLMAP = null;
        private ModuloWIFITE _wIFITE = null;
        private ModuloNOSQL _nOSQL = null;
        private ModuloODAT _oDAT = null;
        private ModuloDNSENUM _dNSENUM = null;
        private ModuloOPENVAS _oPENVAS = null;
        private ModuloTHEHARVESTER _tHEHARVESTER = null;
        private ModuloAMASS _aMASS = null;

        public ModuloNMAP NMAP { get => _nMAP; set => _nMAP = value; }
        public ModuloNESSUS NESSUS { get => _nESSUS; set => _nESSUS = value; }
        public ModuloDNSRECON DNSRECON { get => _dNSRECON; set => _dNSRECON = value; }
        public ModuloFIERCE FIERCE { get => _fIERCE; set => _fIERCE = value; }
        public ModuloDROOPE DROOPE { get => _dROOPE; set => _dROOPE = value; }
        public ModuloJOOMSCAN JOOMSCAN { get => _jOOMSCAN; set => _jOOMSCAN = value; }
        public ModuloWPSCAN WPSCAN { get => _wPSCAN; set => _wPSCAN = value; }
        public ModuloINFOGA INFOGA { get => _iNFOGA; set => _iNFOGA = value; }
        public ModuloOPENDOOR OPENDOOR { get => _oPENDOOR; set => _oPENDOOR = value; }
        public ModuloINFOGAEMAIL INFOGAEMAIL { get => _iNFOGAEMAIL; set => _iNFOGAEMAIL = value; }
        public ModuloSUBLIST3R SUBLIST3R { get => _sUBLIST3R; set => _sUBLIST3R = value; }
        public ModuloWASCAN WASCAN { get => _wASCAN; set => _wASCAN = value; }
        public ModuloWAPITI WAPITI { get => _wAPITI; set => _wAPITI = value; }
        public ModuloSQLMAP SQLMAP { get => _sQLMAP; set => _sQLMAP = value; }
        public ModuloWIFITE WIFITE { get => _wIFITE; set => _wIFITE = value; }
        public ModuloNOSQL NOSQL { get => _nOSQL; set => _nOSQL = value; }
        public ModuloODAT ODAT { get => _oDAT; set => _oDAT = value; }
        public ModuloDNSENUM DNSENUM { get => _dNSENUM; set => _dNSENUM = value; }
        public ModuloOPENVAS OPENVAS { get => _oPENVAS; set => _oPENVAS = value; }
        public ModuloTHEHARVESTER THEHARVESTER { get => _tHEHARVESTER; set => _tHEHARVESTER = value; }
        public ModuloAMASS AMASS { get => _aMASS; set => _aMASS = value; }


        public EditModuloVM()
        {
            NMAP = new ModuloNMAP();
            NESSUS = new ModuloNESSUS();
            DNSRECON = new ModuloDNSRECON();
            FIERCE = new ModuloFIERCE();
            DROOPE = new ModuloDROOPE();
            JOOMSCAN = new ModuloJOOMSCAN();
            WPSCAN = new ModuloWPSCAN();
            INFOGA = new ModuloINFOGA();
            INFOGAEMAIL = new ModuloINFOGAEMAIL();
            SUBLIST3R = new ModuloSUBLIST3R();
            WAPITI = new ModuloWAPITI();
            SQLMAP = new ModuloSQLMAP();
            WIFITE = new ModuloWIFITE();
            OPENDOOR = new ModuloOPENDOOR();
            WASCAN = new ModuloWASCAN();
            NOSQL = new ModuloNOSQL();
            ODAT = new ModuloODAT();
            DNSENUM = new ModuloDNSENUM();
            OPENVAS = new ModuloOPENVAS();
            THEHARVESTER = new ModuloTHEHARVESTER();
            AMASS = new ModuloAMASS();


        }
        public EditModuloVM(Modulo mod)
        {
            if (mod is ModuloNMAP)
            {

                this.NMAP = (ModuloNMAP)mod;
            }
            if (mod is ModuloNESSUS)
            {

                this.NESSUS = (ModuloNESSUS)mod;
            }
            if (mod is ModuloOPENVAS)
            {

                this.OPENVAS = (ModuloOPENVAS)mod;
            }
            if (mod is ModuloFIERCE)
            {

                this.FIERCE = (ModuloFIERCE)mod;
            }
            if (mod is ModuloDNSRECON)
            {

                this.DNSRECON = (ModuloDNSRECON)mod;
            }
            if (mod is ModuloDROOPE)
            {

                this.DROOPE = (ModuloDROOPE)mod;
            }
            if (mod is ModuloJOOMSCAN)
            {

                this.JOOMSCAN = (ModuloJOOMSCAN)mod;
            }
            if (mod is ModuloWPSCAN)
            {

                this.WPSCAN = (ModuloWPSCAN)mod;
            }
            if (mod is ModuloINFOGA)
            {

                this.INFOGA = (ModuloINFOGA)mod;
            }
            if (mod is ModuloINFOGAEMAIL)
            {

                this.INFOGAEMAIL = (ModuloINFOGAEMAIL)mod;
            }
            if (mod is ModuloOPENDOOR)
            {

                this.OPENDOOR = (ModuloOPENDOOR)mod;
            }
            if (mod is ModuloSUBLIST3R)
            {

                this.SUBLIST3R = (ModuloSUBLIST3R)mod;
            }
            if (mod is ModuloWASCAN)
            {

                this.WASCAN = (ModuloWASCAN)mod;
            }
            if (mod is ModuloWAPITI)
            {

                this.WAPITI = (ModuloWAPITI)mod;
            }
            if (mod is ModuloSQLMAP)
            {

                this.SQLMAP = (ModuloSQLMAP)mod;
            }
            if (mod is ModuloWIFITE)
            {

                this.WIFITE = (ModuloWIFITE)mod;
            }
            if (mod is ModuloNOSQL)
            {

                this.NOSQL = (ModuloNOSQL)mod;
            }
            if (mod is ModuloODAT)
            {

                this.ODAT = (ModuloODAT)mod;
            }
            if (mod is ModuloDNSENUM)
            {

                this.DNSENUM = (ModuloDNSENUM)mod;
            }
             if (mod is ModuloTHEHARVESTER)
            {

                this.THEHARVESTER = (ModuloTHEHARVESTER)mod;
            }
            if (mod is ModuloAMASS)
            {

                this.AMASS = (ModuloAMASS)mod;
            }
        }
        public EditModuloVM(ModuloNMAP mod)
        {
            NMAP = mod;
            NESSUS = null;
            DNSRECON = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            INFOGA = null;
            WAPITI = null;
            INFOGAEMAIL = null;
            SUBLIST3R = null;
            SQLMAP = null;
            WIFITE = null;
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
            ODAT = null;
            DNSENUM = null;
            OPENVAS = null;
            THEHARVESTER = null;
            AMASS = null;

        }
        public EditModuloVM(ModuloNESSUS mod)
        {
            NMAP = null;
            DNSRECON = null;
            NESSUS = mod;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            INFOGA = null;
            WAPITI = null;
            INFOGAEMAIL = null;
            SUBLIST3R = null;
            SQLMAP = null;
            FIERCE = null;
            WIFITE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
            ODAT = null;
            DNSENUM = null;
            OPENVAS = null;
            THEHARVESTER = null;
            AMASS = null;


        }
        public EditModuloVM(ModuloDNSRECON mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = mod;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            INFOGA = null;
            WAPITI = null;
            INFOGAEMAIL = null;
            SUBLIST3R = null;
            SQLMAP = null;
            WIFITE = null;
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
            ODAT = null;
            DNSENUM = null;
            OPENVAS = null;
            THEHARVESTER = null;
            AMASS = null;

        }
        public EditModuloVM(ModuloDROOPE mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = mod;
            JOOMSCAN = null;
            WPSCAN = null;
            WAPITI = null;
            INFOGAEMAIL = null;
            SUBLIST3R = null;
            SQLMAP = null;
            WIFITE = null;
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
            ODAT = null;
            DNSENUM = null;
            OPENVAS = null;
            THEHARVESTER = null;
            AMASS = null;

        }
        public EditModuloVM(ModuloJOOMSCAN mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = mod;
            WPSCAN = null;
            WAPITI = null;
            INFOGAEMAIL = null;
            SUBLIST3R = null;
            SQLMAP = null;
            WIFITE = null;
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
            ODAT = null;
            DNSENUM = null;
            OPENVAS = null;
            THEHARVESTER = null;
            AMASS = null;

        }
        public EditModuloVM(ModuloINFOGA mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = mod;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            WAPITI = null;
            INFOGAEMAIL = null;
            SUBLIST3R = null;
            SQLMAP = null;
            WIFITE = null;
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
            ODAT = null;
            DNSENUM = null;
            OPENVAS = null;
            THEHARVESTER = null;
            AMASS = null;

        }
        public EditModuloVM(ModuloINFOGAEMAIL mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            WAPITI = null;
            INFOGAEMAIL = mod;
            SUBLIST3R = null;
            SQLMAP = null;
            WIFITE = null;
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
            ODAT = null;
            DNSENUM = null;
            OPENVAS = null;
            THEHARVESTER = null;
            AMASS = null;

        }
        public EditModuloVM(ModuloSUBLIST3R mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            WAPITI = null;
            INFOGAEMAIL = null;
            SUBLIST3R = mod;
            SQLMAP = null;
            WIFITE = null;
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
            ODAT = null;
            DNSENUM = null;
            OPENVAS = null;
            THEHARVESTER = null;
            AMASS = null;
        }
        public EditModuloVM(ModuloWAPITI mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            WAPITI = mod;
            INFOGAEMAIL = null;
            SUBLIST3R = null;
            SQLMAP = null;
            WIFITE = null;
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
            ODAT = null;
            DNSENUM = null;
            OPENVAS = null;
            THEHARVESTER = null;
            AMASS = null;

        }
        public EditModuloVM(ModuloSQLMAP mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            WAPITI = null;
            INFOGAEMAIL = null;
            SUBLIST3R = null;
            SQLMAP = mod;
            WIFITE = null;
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
            ODAT = null;
            DNSENUM = null;
            OPENVAS = null;
            THEHARVESTER = null;
            AMASS = null;

        }

        public EditModuloVM(ModuloWIFITE mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            WAPITI = null;
            INFOGAEMAIL = null;
            SUBLIST3R = null;
            SQLMAP = null;
            WIFITE = mod;
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
            ODAT = null;
            DNSENUM = null;
            OPENVAS = null;
            THEHARVESTER = null;
            AMASS = null;

        }

        public EditModuloVM(ModuloWPSCAN mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = mod;
            WAPITI = null;
            INFOGAEMAIL = null;
            SUBLIST3R = null;
            SQLMAP = null;
            WIFITE = null;
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
            ODAT = null;
            DNSENUM = null;
            OPENVAS = null;
            THEHARVESTER = null;
            AMASS = null;

        }

        public EditModuloVM(ModuloFIERCE mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            WAPITI = null;
            INFOGAEMAIL = null;
            SUBLIST3R = null;
            SQLMAP = null;
            WIFITE = null;
            FIERCE = mod;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
            ODAT = null;
            DNSENUM = null;
            OPENVAS = null;
            THEHARVESTER = null;
            AMASS = null;

        }

        public EditModuloVM(ModuloOPENDOOR mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            WAPITI = null;
            INFOGAEMAIL = null;
            SUBLIST3R = null;
            SQLMAP = null;
            WIFITE = null;
            FIERCE = null;
            OPENDOOR = mod;
            WASCAN = null;
            NOSQL = null;
            ODAT = null;
            DNSENUM = null;
            OPENVAS = null;
            THEHARVESTER = null;
            AMASS = null;

        }
        public EditModuloVM(ModuloWASCAN mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            WAPITI = null;
            INFOGAEMAIL = null;
            SUBLIST3R = null;
            SQLMAP = null;
            WIFITE = null;
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = mod;
            NOSQL = null;
            ODAT = null;
            DNSENUM = null;
            OPENVAS = null;
            THEHARVESTER = null;
            AMASS = null;

        }
        public EditModuloVM(ModuloNOSQL mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            WAPITI = null;
            INFOGAEMAIL = null;
            SUBLIST3R = null;
            SQLMAP = null;
            WIFITE = null;
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = mod;
            ODAT = null;
            DNSENUM = null;
            OPENVAS = null;
            THEHARVESTER = null;
            AMASS = null;

        }

        public EditModuloVM(ModuloODAT mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            WAPITI = null;
            INFOGAEMAIL = null;
            SUBLIST3R = null;
            SQLMAP = null;
            WIFITE = null;
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
            ODAT = mod;
            DNSENUM = null;
            OPENVAS = null;
            THEHARVESTER = null;
            AMASS = null;

        }

        public EditModuloVM(ModuloDNSENUM mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            WAPITI = null;
            INFOGAEMAIL = null;
            SUBLIST3R = null;
            SQLMAP = null;
            WIFITE = null;
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
            ODAT = null;
            DNSENUM = mod;
            OPENVAS = null;
            THEHARVESTER = null;
            AMASS = null;

        }
        public EditModuloVM(ModuloOPENVAS mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            WAPITI = null;
            INFOGAEMAIL = null;
            SUBLIST3R = null;
            SQLMAP = null;
            WIFITE = null;
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
            ODAT = null;
            DNSENUM = null;
            OPENVAS = mod;
            THEHARVESTER = null;
            AMASS = null;


        }
        public EditModuloVM(ModuloTHEHARVESTER mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            WAPITI = null;
            INFOGAEMAIL = null;
            SUBLIST3R = null;
            SQLMAP = null;
            WIFITE = null;
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
            ODAT = null;
            DNSENUM = null;
            OPENVAS = null;
            THEHARVESTER = mod;
            AMASS = null;

        }
        public EditModuloVM(ModuloAMASS mod)
        {
            NMAP = null;
            NESSUS = null;
            DNSRECON = null;
            INFOGA = null;
            DROOPE = null;
            JOOMSCAN = null;
            WPSCAN = null;
            WAPITI = null;
            INFOGAEMAIL = null;
            SUBLIST3R = null;
            SQLMAP = null;
            WIFITE = null;
            FIERCE = null;
            OPENDOOR = null;
            WASCAN = null;
            NOSQL = null;
            ODAT = null;
            DNSENUM = null;
            OPENVAS = null;
            THEHARVESTER = null;
            AMASS = mod;

        }
    }
}
