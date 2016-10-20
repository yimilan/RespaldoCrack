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
		Thread thread1;
		static internal Thread[] threadAtack;
		Thread multiThread;
		Thread t;

		/*
        * @param Metodo para administrar todos los procesos
        */
		public void origin(){
			read ();
			cantThread ();
			/*for(int i = 0; i<cant; i++)
				generatingDictionary (user[i],paswordHash[i], 8, 1);*/
		}

		/*
        * @param Metodo para ingresar la cantidad de los hilos
        */
		public void cantThread(){
			int num;
			int cantAtack;
			int ult = 0;
			int ver = 0;
			int pos = 1;
			//do {
			Console.Write ("Ingrese la Cantidad de Hilos para tratar el ataque: ");
			num = Convert.ToInt32(Console.ReadLine ());
			Console.WriteLine (".");
			Console.WriteLine (".");
			Console.WriteLine ("Iniciando Ataque!!!");
			Console.WriteLine (".");
			Console.WriteLine (".");
			Console.WriteLine (".");
			Console.WriteLine (".");
			Console.WriteLine (".");
			Console.ReadKey ();
			Console.Clear ();
			cantAtack = (int) (cant / num);
			if ((cantAtack * num) < cant)
				cantAtack += 1;

			threadAtack = new Thread[cantAtack];
			for (int i = 0; i < cantAtack; i++)  
			{ 
				Thread t = new Thread(new ThreadStart (() => (newThread(pos)))); //  <----- problem here
				threadAtack[i]      =      t;      
				threadAtack[i].Name = "Thread-" + i.ToString(); 
			} 
			for (int i = 0; i < cantAtack; i++)  
			{ 
				threadAtack[i].Start();   

			} 
		}

		/*
        * @param Metodo para crear n cantidad de hilos
        */
		public int newThread(int ult){
			int val = 0;

			lock(this)
			{      
				val = generatingDictionary (user[ult],paswordHash[ult], 8, 1);
				Thread.Sleep(5);
				if (val == 1) { 
					return      1;      
				} else {
					return 0;
				}
			}  
			return 0;
		}

		/*
        * @param Metodo para crear diccionario de ataque
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
        * @param Metodo para romper hash
        */

		public int separateForce(String hashBruteForceMD5, String hash){
			int ver = 0;
	
			thread1 = new Thread (() => {
				ver = 0;
				try	{
					ver = verifForce(hashBruteForceMD5,hash);
					if(ver == 1){
						thread1.Abort ();
					}
				}catch (Exception e)
				{
					throw e;
				}
			} );

			thread1.IsBackground = true;
			thread1.Start ();
			thread1.Join ();

	
			if (ver == 1) {
				return 1;
			}

			return 0;
		}

		/*
        * @param Metodo para romper hash
        */
		public int verifForce(String str_FMD5, String str_MD5){
			if (String.Compare(str_FMD5,str_MD5) == 0) {
				return 1;
			}
			return 0;
		}
		/*
        * @param Metodo para crear diccionario de ataque
        */
		public int generatingDictionary(String user, String hash, int tam, int min){
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

			for (int i = 0; i < tamAlpha; i++) {
				bruteF = "";
				bruteF = ("" + alpha[i]);
				hashBruteForceMD5 = codeMD5(bruteF);
				if (separateForce (hashBruteForceMD5, hash) == 1) {
					Console.Write (user);
					Console.WriteLine ("\t" + bruteF);
					Console.ReadKey ();
					return 1;
				}

				for (int j= 0; j < tamAlpha; j++) {
					bruteF = (alpha[i] + "" + alpha[j]);
					hashBruteForceMD5 = codeMD5(bruteF);
					if (separateForce (hashBruteForceMD5, hash) == 1) {
						Console.Write (user);
						Console.WriteLine ("\t" + bruteF);
						Console.ReadKey ();
						return 1;
					}


					for (int k = 0; k < tamAlpha; k++) {
						bruteF = (alpha[i]+ ""  + alpha[j]+ ""  + alpha[k]);
						hashBruteForceMD5 = codeMD5(bruteF);
						if (separateForce (hashBruteForceMD5, hash) == 1) {
							Console.Write (user);
							Console.WriteLine ("\t" + bruteF);
							Console.ReadKey ();
							return 1;
						}
						for (int l = 0; l < tamAlpha; l++) {
							bruteF = (alpha[i]+ ""  + alpha[j]+ ""  + alpha[k]+ ""  + alpha[l]);
							hashBruteForceMD5 = codeMD5(bruteF);
							if (separateForce (hashBruteForceMD5, hash) == 1) {
								Console.Write (user);
								Console.WriteLine ("\t" + bruteF);
								Console.ReadKey ();
								return 1;
							}

							for (int m = 0; m < tamAlpha; m++) {
								bruteF = (alpha[i]+ ""  + alpha[j]+ ""  + alpha[k]+ ""  + alpha[l]+ ""  + alpha[m]);
								hashBruteForceMD5 = codeMD5(bruteF);
								if (separateForce (hashBruteForceMD5, hash) == 1) {
									Console.Write (user);
									Console.WriteLine ("\t" + bruteF);
									Console.ReadKey ();
									return 1;
								}

								for (int n = 0; n < tamAlpha; n++) {
									bruteF = (alpha[i]+ ""  + alpha[j]+ ""  + alpha[k] + "" + alpha[l] + "" + alpha[m] + "" + alpha[n]);
									hashBruteForceMD5 = codeMD5(bruteF);
									if (separateForce (hashBruteForceMD5, hash) == 1) {
										Console.Write (user);
										Console.WriteLine ("\t" + bruteF);
										Console.ReadKey ();
										return 1;
									}

									for (int o = 0; o < tamAlpha; o++) {
										bruteF = (alpha[i] + "" + alpha[j] + "" + alpha[k] + "" + alpha[l] + "" + alpha[m] + "" + alpha[n] + "" + alpha[o]);
										hashBruteForceMD5 = codeMD5(bruteF);
										if (separateForce (hashBruteForceMD5, hash) == 1) {
											Console.Write (user);
											Console.WriteLine ("\t" + bruteF);
											Console.ReadKey ();
											return 1;
										}

										for (int p = 0; p < tamAlpha; p++) {
											bruteF = (alpha[i] + "" + alpha[j] + "" + alpha[k] + "" + alpha[l] + "" + alpha[m] + "" + alpha[n] + "" + alpha[o] + "" + alpha[p]);
											hashBruteForceMD5 = codeMD5(bruteF);
											if (separateForce (hashBruteForceMD5, hash) == 1) {
												Console.Write (user);
												Console.WriteLine ("\t" + bruteF);
												Console.ReadKey ();
												return 1;
											}
										}
									}
								}
							}
						}
					}
				}
			}

			return 0;
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


	}
}
