using System;
using System.Collections.Generic;
using City_Center.Page.SlideMenu;
using City_Center.ViewModels;
using Xamarin.Forms;
using City_Center.Clases;
using City_Center.Database;

namespace City_Center.Page
{
    public partial class DetailPage : ContentPage
    {
        public DetailPage()
        {
            InitializeComponent();

        }

       async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var Seleccion = e.SelectedItem;

            if (Seleccion != null)
            {
                var nameItem = e.SelectedItem.ToString();

                switch (nameItem)
                {
                    case "INICIO":
                        //Application.Current.MainPage = new MasterPage();
                        listviewMenu.SelectedItem = null;

                        //var db = new DBFire();
                        //await db.saveRoom(new Room() { Name = "ab2" });

                        //Application.Current.Properties["Email"] = "ab2";
                        //await Application.Current.SavePropertiesAsync();

                       // App.NavPage = new NavigationPage(new CustomTabPage()) { BarBackgroundColor = Color.FromHex("#23144B") };

                         
						App.NavPage.BarBackgroundColor=Color.FromHex("#23144B"); 

                        ((MasterPage)Application.Current.MainPage).IsPresented = false;

                        break;
                    case "SHOWS":
                      
                        //Application.Current.MainPage = new MasterPage();
                        listviewMenu.SelectedItem = null;

						App.NavPage.BarBackgroundColor=Color.FromHex("#23144B"); 

                        ((MasterPage)Application.Current.MainPage).IsPresented = false;

                        //if (MainViewModel.GetInstance().Shows == null)
                        //{
                            MainViewModel.GetInstance().Shows = new ShowsViewModel();
                            MainViewModel.GetInstance().EventosItem = new EventosItemViewModel();

                            await((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Show());

                       
                        //}
                        //else
                        //{
                          
                        //    await((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Show());
                        //}

                       break;   
                    case "PROMOCIONES":
                        //Application.Current.MainPage = new MasterPage();

                        listviewMenu.SelectedItem = null;

						App.NavPage.BarBackgroundColor=Color.FromHex("#23144B"); 

                        ((MasterPage)Application.Current.MainPage).IsPresented = false;

                        //if (MainViewModel.GetInstance().Favoritos == null)
                        //{
                        MainViewModel.GetInstance().Promociones = new PromocionesViewModel();

                        await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Promociones());

                        //}
                        //else
                        //{
                        //    await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Favoritos());
                        //}



                       break;
                    case "GUARDADOS":

                         bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                              (bool)Application.Current.Properties["IsLoggedIn"] : false;
                        
                        ((MasterPage)Application.Current.MainPage).IsPresented = false;

                        listviewMenu.SelectedItem = null;

                        if (isLoggedIn)
                        {
							App.NavPage.BarBackgroundColor=Color.FromHex("#23144B"); 

                            MainViewModel.GetInstance().Favoritos = new FavoritosViewModel();
                            MainViewModel.GetInstance().FavoritoItem = new FavoritoItemViewModel();

                            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Favoritos());
                        }
                        else
                        {
                            listviewMenu.SelectedItem = null;
                            await Mensajes.Alerta("Debes de estar logeado para acceder a esta opcion");
                        }


                       break;
                    case "MÁS INFORMACIÓN":
                       
                        //Application.Current.MainPage = new MasterPage();
                        listviewMenu.SelectedItem = null;

						App.NavPage.BarBackgroundColor=Color.FromHex("#23144B"); 

                            ((MasterPage)Application.Current.MainPage).IsPresented = false;

                            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new InfoPage());


                        break; 
                    case "TÉRMINOS Y CONDICIONES GENERALES DE USO":

                        listviewMenu.SelectedItem = null;

                        ((MasterPage)Application.Current.MainPage).IsPresented = false;

						App.NavPage.BarBackgroundColor=Color.FromHex("#23144B"); 

                        await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new TerminosCondiciones());


                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }
                return;
            }

        }
    }
}
