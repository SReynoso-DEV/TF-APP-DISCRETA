using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Animation;


namespace TF_discreta
{
    [Activity(Label = "DEFINIENDO COMPATIBILIDADES")]
    public class Showing : Activity
    {
        
        MatrizQ ma; //matriz                  
        int numcant;
        int Nelem = 0;
        int[][] mat;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Showing);

            string cant = Intent.GetStringExtra("counter" ?? "error");
            int.TryParse(cant, out numcant);
            string[] myTextViews = new string[numcant];

            var EnumText = FindViewById<TextView>(Resource.Id.enumtext);
            var CantText = FindViewById<TextView>(Resource.Id.TextCant);
            var ButtonBack = FindViewById<Button>(Resource.Id.GoBack);
            var container = FindViewById<LinearLayout>(Resource.Id.madre);
            var Compatible = FindViewById<TextView>(Resource.Id.compatible);
            for (int i = 0; i < numcant; i++)
            {
                myTextViews[i] = Intent.GetStringExtra($"element{i}" ?? "error");
            }

            CantText.Text = myTextViews[Nelem];
            //EVENTO
            ma = new MatrizQ(numcant);
            mat = ma.returnMa();
            CheckBox[] myRButtons = new CheckBox[1000];
            TextView[] myTextViews2 = new TextView[1000];
            View[] myAddViews = new View[100];

            void Evento(int Nelem)
            {
               
                 CantText.Text = myTextViews[Nelem];
                for (int j = Nelem + 1; j < numcant; j++)
                {
                    LayoutInflater inflate = (LayoutInflater)BaseContext.GetSystemService(Context.LayoutInflaterService);
                    View addView2 = LayoutInflater.Inflate(Resource.Layout.buttons, null);
                    TextView nombreE = addView2.FindViewById<TextView>(Resource.Id.nombre2);
                    nombreE.Text = myTextViews[j];
                    CheckBox compE = addView2.FindViewById<CheckBox>(Resource.Id.comp);
                    myRButtons[j] = compE;
                    myTextViews2[j] = nombreE;
                    myAddViews[j] = addView2;
                    container.AddView(addView2);
                    
                    compE.CheckedChange += delegate{};
                }

            }

            Evento(Nelem);
            ButtonBack.Click += delegate
            {
                if (Nelem < numcant-1)
                {
                    for (int j = Nelem + 1; j < numcant; j++)
                    {
                        if (true == myRButtons[j].Checked)
                        {
                            ma.setP(Nelem, j, 1);
                        }
                        else
                        {
                            ma.setP(Nelem, j, 0);
                        }
                    }

                    CantText.Text = String.Empty;

                    for (int j = Nelem + 1; j < numcant; j++)
                    {
                        myTextViews2[j].Visibility = ViewStates.Gone;
                        myRButtons[j].Visibility = ViewStates.Gone;
                        myAddViews[j].Visibility = ViewStates.Gone;
                    }
                    CantText.Text = String.Empty;
                    Nelem++;
                    Evento(Nelem);
                    if (Nelem == numcant-1)
                    {
                        Compatible.Visibility = ViewStates.Gone;
                        ButtonBack.Text = "Agrupar elementos químicos";
                    }

                }

                else
                {

                    EnumText.Visibility = ViewStates.Gone;
                    CantText.Visibility = ViewStates.Gone;
                    ButtonBack.Visibility = ViewStates.Gone;
                    container.Visibility = ViewStates.Gone;
                    if (numcant >= 2)
                    {
                        this.Title = "ELEMENTOS AGRUPADOS";

                        ma.setMat(mat);
                        ma.Calc();
                        int[] arr = ma.getArr();
                        //ma.Ordenar();
                        int aux2;
                        string aux3;
                        for (int i = 0; i < numcant; i++)
                        {
                            for (int j = 0; j < numcant; j++)
                            {
                                if (arr[i] < arr[j])
                                {
                                    aux2 = arr[i];
                                    arr[i] = arr[j];
                                    arr[j] = aux2;
                                    aux3 = myTextViews[i];
                                    myTextViews[i] = myTextViews[j];
                                    myTextViews[j] = aux3;
                                }
                            }

                        }
                        EnumText.Visibility = ViewStates.Visible;
                        EnumText.Text = String.Empty;
                        //container.Visibility = ViewStates.Visible;
                        for (int i = 0; i < ma.getHab(); i++)
                        {
                            int aux4 = i + 1;
                            EnumText.SetTextColor(Android.Graphics.Color.Red);
                            EnumText.Text = EnumText.Text + "Los químicos en el cuarto #" + aux4 + ": \n";
                            //EnumText.SetTextColor(Android.Graphics.Color.White);
                            for (int j = 0; j < numcant; j++)
                            {
                                if (arr[j] == i)
                                {
                                    //View addView2 = LayoutInflater.Inflate(Resource.Layout.buttons, null);
                                    //TextView nombreE = addView2.FindViewById<TextView>(Resource.Id.nombre2);
                                    //nombreE.Text = myTextViews[j];
                                    //container.AddView(addView2);
                                    //EnumText.Text = EnumText.Text + "\n";
                                    EnumText.Text = EnumText.Text + myTextViews[j] + "\n";
                                }
                            }
                            EnumText.Text = EnumText.Text + "\n";
                        }


                    }
                }
            };
            LayoutTransition trans = new LayoutTransition();
            container.LayoutTransition = trans;
        }
    }
}