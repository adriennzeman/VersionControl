using Mikroszimulacio.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikroszimulacio
{
    public partial class Form1 : Form
    {
        List<Person> Population;
        List<BirthProbabilities> BirthProbabilities;
        List<DeathProbabilities> DeathProbabilities;
        List<int> CntMale;
        List<int> CntFemale;
        Random rng = new Random(1234);

        public Form1()
        {
            InitializeComponent();
        }

        private void Simulation(int zaroev, string filename)
        {
            Population = GetPopulation(filename);
            BirthProbabilities = GetBirthProbabilities("C:/Temp/születés.csv");
            DeathProbabilities = GetDeathProbabilities("C:/Temp/halál.csv");
            CntMale = new List<int>();
            CntFemale = new List<int>();

            for (int year = 2005; year <= zaroev; year++)
            {
                for (int i = 0; i < Population.Count; i++)
                {
                    SzimulaciosLepes(year, Population[i]);
                }

                int ferfiakSzama = (from x in Population where x.Gender == Gender.Male select x).Count();
                int nokSzama = (from x in Population where x.Gender == Gender.Female select x).Count();
                Console.WriteLine(string.Format("Ev: {0}, Ferfiak: {1}, Nok: {2}", year, ferfiakSzama, nokSzama));
                CntMale.Add(ferfiakSzama);
                CntFemale.Add(nokSzama);
            }
        }

        private void SzimulaciosLepes(int year, Person person)
        {
            if (!person.IsAlive) return;
            byte age = (byte)(year - person.BirthYear);
            double pDeath = (from x in DeathProbabilities
                             where x.Gender == person.Gender && x.Age == age
                             select x.P).FirstOrDefault();
            if (rng.NextDouble() <= pDeath)
                person.IsAlive = false;
            if (person.IsAlive && person.Gender == Gender.Female)
            {
                double pBirth = (from x in BirthProbabilities
                                 where x.Age == age 
                                 && x.NbrOfChildren == person.NbrOfChildren
                                 select x.P).FirstOrDefault();
                if (rng.NextDouble() <= pBirth)
                {
                    Person újszülött = new Person();
                    újszülött.BirthYear = year;
                    újszülött.NbrOfChildren = 0;
                    újszülött.Gender = (Gender)(rng.Next(1, 3));
                    Population.Add(újszülött);
                }
            }
        }

        public List<Person> GetPopulation(string csvpath)
        {
            List <Person> population = new List<Person>();
            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    });
                }
            }
            return population;
        }

        public List<BirthProbabilities> GetBirthProbabilities(string csvpath)
        {
            List<BirthProbabilities > birthProbabilities = new List<BirthProbabilities>();
            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    birthProbabilities.Add(new BirthProbabilities()
                    {
                        Age = int.Parse(line[0]),
                        NbrOfChildren = int.Parse(line[1]),
                        P = double.Parse(line[2].Replace(',','.'))

                    });
                }
            }
            return birthProbabilities;
        }

        public List<DeathProbabilities> GetDeathProbabilities(string csvpath)
        {
            List<DeathProbabilities> deathProbabilities = new List<DeathProbabilities>();
            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    deathProbabilities.Add(new DeathProbabilities()
                    {
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[0]),
                        Age = int.Parse(line[1]),
                        P = double.Parse(line[2].Replace(',', '.'))

                    });
                }
            }
            return deathProbabilities;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            rtbResults.Text = "";
            Simulation((int)numericUpDown1.Value, tbFilename.Text);
            DisplayResults((int)numericUpDown1.Value);

        }

        private void DisplayResults(int zaroev)
        {
            int counter = 0;
            for (int i = 2005; i<= zaroev; i++) 
            {
                rtbResults.Text += String.Format("Szimulacios ev: {0}\n", i);
                rtbResults.Text += String.Format("\tferfiak: {0}\n\tnok: {1}\n\n", CntMale[counter], CntFemale[counter]);
                counter++; 
            }

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                tbFilename.Text = ofd.FileName;
            }
        }
    }
}
