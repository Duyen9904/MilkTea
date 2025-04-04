using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supabase;


namespace PRN222.Assignment.Services.Implementations
{
    public class SupabaseService
    {
        private readonly Supabase.Client _client;

        public SupabaseService()
        {
            _client = new Supabase.Client("https://nzwbqkclgdzqndmpthlq.supabase.co", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im56d2Jxa2NsZ2R6cW5kbXB0aGxxIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDM3Mjc3NjQsImV4cCI6MjA1OTMwMzc2NH0.I9TgNvKfIOO_UPVCdEOFnddvFPjRXwtso1fD6dbV9Bc");
        }
    }
}
