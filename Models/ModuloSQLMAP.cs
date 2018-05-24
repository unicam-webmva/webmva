using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{

    public enum VERBOSESQLMAP
    {
        UNO, DUE, TRE, QUATTRO, CINQUE, SEI, ZERO
    }
    public enum TORTYPE
    {
        HTTP, SOCKS4, SOCKS5
    }
    public enum DETECTIONLEVEL
    {
        UNO, DUE, TRE, QUATTRO, CINQUE
    }
    public enum RISCHIO
    {
        UNO, DUE, TRE
    }
    public class ModuloSQLMAP : Modulo
    {
        public string URL { get; set; }
        public string connectionString { get; set; }

        public string header { get; set; }
        public string credenzialiAutenticazione { get; set; }
        public string dbms { get; set; }
        public string dbmsCredenziali { get; set; }
        public string sistemaOperativo { get; set; }
        public bool forceAggressive { get; set; }
        public bool tor { get; set; }
        public bool checkTor { get; set; }
        public bool forceSsl { get; set; }
        public bool allOptimization { get; set; }
        public bool predictOutput { get; set; }
        public bool keepAlive { get; set; }
        public bool nessunaConnessione { get; set; }
        public bool a { get; set; }
        public bool b { get; set; }
        public bool currentUser { get; set; }
        public bool currentDb { get; set; }
        public bool hostName { get; set; }
        public bool users { get; set; }
        public bool passwords { get; set; }
        public bool privileges { get; set; }
        public bool roles { get; set; }
        public bool dbs { get; set; }
        public bool tables { get; set; }
        public bool columns { get; set; }
        public bool schema { get; set; }
        public bool count { get; set; }
        public bool dumpAll { get; set; }
        public bool search { get; set; }
        public bool excludesSySdbs { get; set; }
        public bool commonTables { get; set; }
        public bool commonColumns { get; set; }

        public int porta { get; set; }
        public int delay { get; set; }
        private int _timeout = 30;
        public int timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
            }
        }
        private int _retries = 3;
        public int retries
        {
            get
            {
                return _retries;
            }
            set
            {
                _retries = value;
            }
        }
        private int _threads = 1;
        public int threads
        {
            get
            {
                return _threads;
            }
            set
            {
                _threads = value;
            }
        }
        private int _timesec = 5;
        public int timesec
        {
            get
            {
                return _timesec;
            }
            set
            {
                _timesec = value;
            }
        }
        private string _tecnique = "BEUSTQ";
        public string tecnique
        {
            get
            {
                return _tecnique;
            }
            set
            {
                _tecnique = value;
            }
        }


        private VERBOSESQLMAP _verbose = VERBOSESQLMAP.UNO;
        public VERBOSESQLMAP Verbose
        {
            get { return _verbose; }
            set { _verbose = value; }
        }
        private TORTYPE _torType = TORTYPE.SOCKS5;
        public TORTYPE TorType
        {
            get { return _torType; }
            set { _torType = value; }
        }
        private DETECTIONLEVEL _detection = DETECTIONLEVEL.UNO;
        public DETECTIONLEVEL Detection
        {
            get { return _detection; }
            set { _detection = value; }
        }
        private RISCHIO _rischio = RISCHIO.UNO;
        public RISCHIO Rischio
        {
            get { return _rischio; }
            set { _rischio = value; }
        }
        public string ComandoPersonalizzato { get; set; }
        public override string Comando
        {
            get
            {
                if (string.IsNullOrEmpty(ComandoPersonalizzato))
                {
                    string risultato = "sqlmap --batch";

                    if (!string.IsNullOrEmpty(connectionString))
                        risultato += " -d " + connectionString;
                    else if (!string.IsNullOrEmpty(URL))
                    {
                        risultato += " -u " + URL;
                    }
                    else
                    {
                        return "Non hai inserito i parametri nel modo giusto!";
                    }
                    if (forceAggressive)
                        risultato += " -f";
                    if (!string.IsNullOrEmpty(header))
                        risultato += " -h " + header;
                    if (!string.IsNullOrEmpty(credenzialiAutenticazione))
                        risultato += " --auth-cred=" + credenzialiAutenticazione;
                    if (tor)
                        risultato += " --tor";
                    if (porta != 0)
                        risultato += " --tor-port=" + porta;
                    switch (Verbose)
                    {
                        case VERBOSESQLMAP.ZERO:
                            risultato += " -v 0";
                            break;
                        case VERBOSESQLMAP.DUE:
                            risultato += " -v 2";
                            break;
                        case VERBOSESQLMAP.TRE:
                            risultato += " -v 3";
                            break;
                        case VERBOSESQLMAP.QUATTRO:
                            risultato += " -v 4";
                            break;
                        case VERBOSESQLMAP.CINQUE:
                            risultato += " -v 5";
                            break;
                        case VERBOSESQLMAP.SEI:
                            risultato += " -v 6";
                            break;
                        default:
                            break;
                    }
                    switch (TorType)
                    {
                        case TORTYPE.HTTP:
                            risultato += " --tor-type=HTTP";
                            break;
                        case TORTYPE.SOCKS4:
                            risultato += " --tor-type=SOCKS4";
                            break;
                        default:
                            break;
                    }
                    switch (Rischio)
                    {
                        case RISCHIO.DUE:
                            risultato += " --risk=2";
                            break;
                        case RISCHIO.TRE:
                            risultato += " --risk=3";
                            break;
                        default:
                            break;
                    }
                    switch (Detection)
                    {
                        case DETECTIONLEVEL.DUE:
                            risultato += " --level=2";
                            break;
                        case DETECTIONLEVEL.TRE:
                            risultato += "  --level=3";
                            break;
                        case DETECTIONLEVEL.QUATTRO:
                            risultato += "  --level=4";
                            break;
                        case DETECTIONLEVEL.CINQUE:
                            risultato += "  --level=5";
                            break;
                        default:
                            break;
                    }
                    if (checkTor)
                        risultato += " --check-tor";
                    if (delay > 0)
                        risultato += " --delay=" + delay;
                    if (timeout != 30 && timeout > 0)
                        risultato += " --timeout=" + timeout;
                    if (retries != 3 && retries > 0)
                        risultato += " --retries=" + retries;
                    if (forceSsl)
                        risultato += " --force-ssl";
                    if (allOptimization)
                        risultato += " -o ";
                    if (predictOutput)
                        risultato += " --predict-output";
                    if (keepAlive)
                        risultato += " --keep-alive";
                    if (nessunaConnessione)
                        risultato += " --null-connection";
                    if (threads > 1)
                        risultato += " --retries=" + threads;
                    if (!string.IsNullOrEmpty(dbms))
                        risultato += " --dbms" + dbms;
                    if (!string.IsNullOrEmpty(dbmsCredenziali))
                        risultato += " --dbms-cred=" + dbmsCredenziali;
                    if (!string.IsNullOrEmpty(sistemaOperativo))
                        risultato += " --os=" + sistemaOperativo;
                    if (timesec != 5 && timesec > 0)
                        risultato += " --time-sec=" + timesec;
                    if (!string.IsNullOrEmpty(tecnique) && tecnique.Equals("BEUSTQ"))
                        risultato += " --tecnique=" + tecnique;
                    if (a)
                        risultato += " -a";
                    if (b)
                        risultato += " -b";
                    if (currentUser)
                        risultato += " --current-user";
                    if (currentDb)
                        risultato += " --current-db";
                    if (hostName)
                        risultato += " --hostname";
                    if (users)
                        risultato += " --users";
                    if (passwords)
                        risultato += " --passwords";
                    if (privileges)
                        risultato += " --privileges";
                    if (roles)
                        risultato += " --roles";
                    if (dbs)
                        risultato += " --dbs";
                    if (tables)
                        risultato += " --tables";
                    if (columns)
                        risultato += " --columns";
                    if (schema)
                        risultato += " --schema";
                    if (dumpAll)
                        risultato += " --dump-all";
                    if (search)
                        risultato += " --search";
                    if (excludesSySdbs)
                        risultato += " --exclude-sysdbs";
                    if (commonTables)
                        risultato += " --common-tables";
                    if (commonColumns)
                        risultato += " --common-columns";


                    return risultato;



                }
                else return ComandoPersonalizzato;
            }

        }
    }
}