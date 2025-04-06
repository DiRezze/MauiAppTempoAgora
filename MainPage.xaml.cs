using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using System.Threading.Tasks;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Pesquisar(object sender, EventArgs e)
        {
            try
            {
                string q = txt_cidade.Text;
                if (!string.IsNullOrEmpty(q))
                {
                    Tempo? t = await DataService.GetPrevisao(q);

                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n" +
                                         $"Tempo: {t.main} \n" +
                                         $"Descrição: {t.description} \n" +
                                         $"Velocidade do vento: {t.speed} \n" +
                                         $"Visibilidade: {t.visibility} \n" +
                                         $"Nascer do sol: {t.sunrise} \n" +
                                         $"Pôr do sol: {t.sunset} \n" +
                                         $"Máxima: {t.temp_max} \n" +
                                         $"Mínima: {t.temp_min} \n";

                        lbl_res.Text = dados_previsao;

                    } else
                    {
                        lbl_res.Text = "Sem dados encontrados";
                    }

                } else
                {
                    lbl_res.Text = "Preencha a consulta";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }

        }
    }

}
