﻿using System;
using System.ComponentModel.DataAnnotations;
namespace webmva.Models
{
    public enum APPLICAZIONE
    {
        NMAP, NESSUS
    }
    /// <summary>
    /// Questo modello rappresenta un Modulo, usabile in più Progetti.
    /// </summary>
    public abstract class Modulo
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public virtual string Comando { get; }
        public APPLICAZIONE Applicazione { get; set; }

    }
}