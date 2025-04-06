using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using System.Net;
using System.Threading.Tasks;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        public async void VerificarConexao()
        {
            if(Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Erro", "Você está sem conexão com a internet.", "OK");
            } else
            {
                await DisplayAlert("Sucesso", "Você está conectado à internet.", "OK");
            }
        }

        private async void Pesquisar(object sender, EventArgs e)
        {
            VerificarConexao(); 

            try
            {
                string q = txt_cidade.Text;

                if (string.IsNullOrWhiteSpace(q))
                {
                    lbl_res.Text = "Preencha a consulta";
                    return;
                }

                var (t, statusCode) = await DataService.GetPrevisao(q);

                if ((int)statusCode == 0)
                {
                    await DisplayAlert("Erro", "Você está sem conexão com a internet.", "OK");
                }
                else if (statusCode == HttpStatusCode.NotFound)
                {
                    lbl_res.Text = "Cidade não encontrada.";
                }
                else if (t != null)
                {
                    string dados_previsao = $"Latitude: {t.lat} \n" +
                                            $"Longitude: {t.lon} \n" +
                                            $"Tempo: {t.main} \n" +
                                            $"Descrição: {t.description} \n" +
                                            $"Velocidade do vento: {t.speed} \n" +
                                            $"Visibilidade: {t.visibility} \n" +
                                            $"Nascer do sol: {t.sunrise} \n" +
                                            $"Pôr do sol: {t.sunset} \n" +
                                            $"Máxima: {t.temp_max}°C \n" +
                                            $"Mínima: {t.temp_min}°C \n";

                    lbl_res.Text = dados_previsao;
                }
                else
                {
                    lbl_res.Text = "Erro ao buscar dados.";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro inesperado", ex.Message, "OK");
            }
        }

    }

}
