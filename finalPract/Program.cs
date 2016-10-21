using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
using System.Collections;
using System.Threading;

namespace Decrypting_CMD
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Origin obj = new Origin ();
			obj.origin ();

		}
	}

	public class Origin{
		/*
        * @param Variables Universales
        */
		static int TAM_MAX = 100;
		String[] user = new String[TAM_MAX];
		String[] paswordHash = new String[TAM_MAX];
		int cant = 0;
		static internal Thread[] threadAtack;
	

		/*
        * @param Metodo para administrar todos los procesos
        */
		public void origin(){
			read ();
			imprimir ();
			Console.ReadKey ();
			Console.Clear ();
			generatingDictionary ();
			Console.WriteLine ("#### Crack Exitoso ####");
			Console.ReadKey ();
		}

		/*
        * @param Metodo para cifrar
        */
		public String codeMD5(String bruteF){
			MD5 md5 = MD5CryptoServiceProvider.Create();
			ASCIIEncoding encoding = new ASCIIEncoding();
			byte[] stream = null;
			StringBuilder sb = new StringBuilder();
			stream = md5.ComputeHash(encoding.GetBytes(bruteF));
			for (int i = 0; i < stream.Length; i++) {
				sb.AppendFormat ("{0:x2}", stream [i]);
			}
			return sb.ToString();
		}

		/*
        * @param Metodo para crear diccionario de ataque
        */
		public void generatingDictionary(){
			char[] alpha = {'a','b','c','d','e','f','g','h','i',
				'j','k','l','m','n','ñ','o','p','q',
				'r','s','t','u','v','w','x','y','z',
				'A','B','C','D','E','F','G','H','I',
				'J','K','L','M','N','Ñ','O','P','Q',
				'R','S','T','U','V','W','X','Y','Z'};
			int tamAlpha = alpha.Length;
			String bruteF;
			String hashBruteForceMD5 = "";
			bruteF = "";
			int pos = 0;
			bool rev;
			int contCrack = 0;

			for (int i = 0; i < tamAlpha; i++) {
				bruteF = "";
				bruteF = ("" + alpha[i]);
				//Console.WriteLine (bruteF);
				hashBruteForceMD5 = codeMD5(bruteF);
				rev = compare (hashBruteForceMD5,ref pos);
				if (rev) {
					look (bruteF, pos);
					contCrack++;
					if (contCrack == cant) {
						return;
					}
				}

				for (int j= 0; j < tamAlpha; j++) {
					bruteF = (alpha[i] + "" + alpha[j]);
					//Console.WriteLine (bruteF);
					hashBruteForceMD5 = codeMD5(bruteF);
					rev = compare (hashBruteForceMD5,ref pos);
					if (rev) {
						look (bruteF, pos);
						contCrack++;
						if (contCrack == cant) {
							return;
						}
					}

					for (int k = 0; k < tamAlpha; k++) {
						bruteF = (alpha[i]+ ""  + alpha[j]+ ""  + alpha[k]);
						//Console.WriteLine (bruteF);
						hashBruteForceMD5 = codeMD5(bruteF);
						rev = compare (hashBruteForceMD5,ref pos);
						if (rev) {
							look (bruteF, pos);
							contCrack++;
							if (contCrack == cant) {
								return;
							}
						}

						for (int l = 0; l < tamAlpha; l++) {
							bruteF = (alpha[i]+ ""  + alpha[j]+ ""  + alpha[k]+ ""  + alpha[l]);
							//Console.WriteLine (bruteF);
							hashBruteForceMD5 = codeMD5(bruteF);
							rev = compare (hashBruteForceMD5,ref pos);
							if (rev) {								
								look (bruteF, pos);
								contCrack++;
								if (contCrack == cant) {
									return;
								}
							}
						}
					}
				}
			}

		}

		/*
        * @param Metodo para apertura y lectura de archivo
         * construcion de ArrayList
        */
		public void read()
		{
			string line;
			String ruta;
			Console.Write ("Introdusca la ruta o arratre el archivo a la consola para obtener ruta: ");
			ruta = Console.ReadLine ();
			Console.WriteLine ();
			System.IO.StreamReader file = new System.IO.StreamReader(@""+ruta);
			while((line = file.ReadLine()) != null)
			{
				Console.WriteLine ("Encrypting: " + line);
				breackString (line);
				cant++;
			}
			file.Close();
		}

		/*
        * @param Metodo para Romper Lineas
        */
		public void breackString(string line){
			char[] delimit = new char[] {':'};
			int cont = 0;
			foreach (String subStr in line.Split(delimit)) {
				if (cont == 2) {					
					Console.WriteLine("Password: " + subStr);
					paswordHash [cant] = subStr;

				} else {
					if (cont == 0) {
						Console.WriteLine ("User: " + subStr);
						user [cant] = subStr;
					}
				}
				cont++;
			}
			Console.WriteLine ();
		}
		/*
        * @param Metodo para imprimir contendo user
        */
		public void imprimir(){
			for (int i = 0; i < cant; i++)
			{
				Console.Write (user [i] + "\t");
				//Console.WriteLine (paswordHash[i]);
			}
		}
		/*
        * @param Metodo para comparar
        */
		public bool compare(String passMD5, ref int pos){
			for (int i = 0; i < cant; i++) {
				//Console.WriteLine (paswordHash [i] + " " + passMD5);
				if(String.Compare(paswordHash[i],passMD5) == 0){
					pos = i;
					return true;
				}
			}
					return false;
		}

		/*
        * @param Metodo para mostrar user Crack
        */
		public void look(String pass, int pos){
			
			Console.WriteLine (user[pos] + "\t" + pass);
			//Console.WriteLine ("_____________________________________________");
				
		}
	}
}
