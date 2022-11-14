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

namespace Sudoku
{
    public partial class Form1 : Form
    {
        List<Sudoku> _sudokus = new List<Sudoku>();
        Random rng = new Random();
        Sudoku _currentQuiz;
        public Form1()
        {
            InitializeComponent();
            CreatePlayField();
            LoadSudokus();
            NewGame();
        }

        void CreatePlayField()
        {
            int LineWidth = 5;
            for (int sor = 0; sor < 9; sor++)
            {
                for (int oszlop = 0; oszlop < 9; oszlop++)
                {
                    SudokuField sf = new SudokuField();
                    sf.Left = oszlop * sf.Width + (int)Math.Floor((double)oszlop / 3)*LineWidth;
                    sf.Top = sor * sf.Width;
                    panel1.Controls.Add(sf);
                }
            }
        }
        void LoadSudokus()
        {
            using (StreamReader sr = new StreamReader("sudoku.csv", Encoding.Default))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    Sudoku beolvasottfeladvany = new Sudoku();
                    string sor = sr.ReadLine();
                    string[] elemek = sor.Split(',');
                    beolvasottfeladvany.Quiz = elemek[0];
                    beolvasottfeladvany.Solution = elemek[1];
                    _sudokus.Add(beolvasottfeladvany);
                }
            }
        }
        Sudoku GetRandomQuiz()
        {
            int veletlenszam = rng.Next(_sudokus.Count);
            return _sudokus[veletlenszam];
        }
        void NewGame()
        {
            int counter = 0;
            _currentQuiz = GetRandomQuiz();
            foreach (var item in panel1.Controls.OfType<SudokuField>())
            {

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
