using GuitarLearning_Essentials;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuitarLearning_Mobile.Pages
{
    /// <summary>
    /// Application page which implements the user interface with which several networking options can be tested.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pgNetworkTesting : ContentPage
    {
        /// <summary>
        /// Static sample data, so the HttpPost can be tested.
        /// </summary>
        private EssentiaModel exampleData { get; } = new EssentiaModel()
        {
            audioData = new float[1000]
            {
                -0.031237809f,-0.021511257f,-0.032088403f,-0.022641655f,-0.032581173f,-0.023812946f,-0.033083204f,-0.025301222f,-0.034203973f,-0.027560223f,
                -0.036059823f,-0.030776756f,-0.038212873f,-0.03463385f,-0.040432964f,-0.038746104f,-0.04269338f,-0.042739388f,-0.044953693f,-0.0463878f,
                -0.04714319f,-0.04975495f,-0.049198437f,-0.052998528f,-0.05117169f,-0.056100488f,-0.053025674f,-0.05866339f,-0.0547498f,-0.06052424f,
                -0.0563253f,-0.06211081f,-0.05766618f,-0.06397792f,-0.05883868f,-0.06628499f,-0.05989144f,-0.06863569f,-0.06093886f,-0.07079281f,
                -0.06224449f,-0.073020466f,-0.0640988f,-0.07565112f,-0.066440605f,-0.07858047f,-0.0685227f,-0.08117835f,-0.069767274f,-0.08308273f,
                -0.07038151f,-0.084451474f,-0.070804074f,-0.08545649f,-0.070854634f,-0.08592895f,-0.069790885f,-0.08551164f,-0.06764813f,-0.08441238f,
                -0.065418534f,-0.083254874f,-0.06376032f,-0.08222414f,-0.062426113f,-0.08100467f,-0.060928944f,-0.07937374f,-0.059416782f,-0.077645205f,
                -0.058316704f,-0.07618952f,-0.057454895f,-0.074857146f,-0.056174688f,-0.07329178f,-0.05405394f,-0.07137126f,-0.051249806f,-0.069142f,-0.048119552f,
                -0.066497035f,-0.04479286f,-0.06325157f,-0.041357875f,-0.05963301f,-0.03795352f,-0.05613444f,-0.03466429f,-0.052995764f,-0.031694174f,-0.05036243f,
                -0.029029207f,-0.048303504f,-0.026207753f,-0.04658665f,-0.022929844f,-0.044826027f,-0.019406244f,-0.04272854f,-0.016279763f,-0.04050318f,-0.013892291f,
                -0.038514685f,-0.011768388f,-0.03658547f,-0.009338713f,-0.034351807f,-0.006560689f,-0.031822443f,-0.0039718035f,-0.029491425f,-0.0020016269f,-0.02763398f,
                -0.00034575013f,-0.02576697f,0.0014666703f,-0.023504186f,0.00356106f,-0.021244265f,0.0056958166f,-0.019778932f,0.00751207f,-0.019378241f,0.008798654f,-0.019566791f,0.009608292f,
                -0.019859947f,0.010419169f,-0.020158675f,0.011675989f,-0.020641893f,0.013301173f,-0.02134772f,0.014859637f,-0.022007693f,0.016002925f,-0.02240725f,0.01687006f,-0.022435345f,0.01774187f,
                -0.022056665f,0.018448308f,-0.021533167f,0.018679705f,-0.02135352f,0.018689556f,-0.021761544f,0.019116167f,-0.022505142f,0.020245958f,-0.023007097f,0.021672718f,-0.02287281f,0.022689959f,
                -0.022301784f,0.023257736f,-0.02160277f,0.02392941f,-0.020791845f,0.024852298f,-0.019841965f,0.02570805f,-0.018802557f,0.02637289f,-0.017817568f,0.027255202f,-0.01695935f,0.028796611f,-0.016033124f,
                0.030866073f,-0.014623637f,0.032945197f,-0.012405772f,0.034697983f,-0.009547087f,0.036217883f,-0.006597039f,0.03774146f,-0.003938119f,0.039380558f,-0.0014846036f,0.04105488f,0.0009523629f,0.042573176f,
                0.0033558593f,0.043705136f,0.005625683f,0.044375043f,0.0077625355f,0.044954114f,0.009901462f,0.045602895f,0.0118199065f,0.045944933f,0.013182759f,0.045692526f,0.014258445f,0.044781502f,0.015421872f,
                0.043457508f,0.016673597f,0.042130824f,0.017749276f,0.041121352f,0.018539514f,0.040715642f,0.019401576f,0.04092988f,0.020709448f,0.041479174f,0.022533545f,0.041877713f,0.0245621f,0.041639306f,0.02624021f,
                0.04064121f,0.027250348f,0.039164398f,0.02773573f,0.03771747f,0.028123971f,0.036511708f,0.028576594f,0.0352503f,0.028925749f,0.033658266f,0.02923432f,0.0319275f,0.029883824f,0.030582786f,0.0311206f,
                0.029920494f,0.032785997f,0.029755708f,0.03460606f,0.029617872f,0.03634929f,0.02916393f,0.03768451f,0.028505838f,0.038386203f,0.027900742f,0.038608808f,0.027232222f,0.03868704f,0.02612454f,0.03871131f,
                0.024512026f,0.03848926f,0.022746151f,0.037868652f,0.021199044f,0.0370387f,0.019919068f,0.03641727f,0.018726008f,0.03628537f,0.017605826f,0.036679864f,0.016601037f,0.037337366f,0.015696835f,0.037947375f,
                0.015050396f,0.038519382f,0.014765697f,0.039087333f,0.014526359f,0.03937764f,0.013725668f,0.03902362f,0.012151682f,0.03806604f,0.01035685f,0.0369894f,0.008959698f,0.036059808f,0.007878414f,0.035030056f,
                0.0065891477f,0.03368084f,0.0050176326f,0.03235295f,0.0037847273f,0.031678453f,0.003426398f,0.031877045f,0.0036411127f,0.032496843f,0.0036777738f,0.03291076f,0.0033775868f,0.03298764f,0.0032514462f,
                0.033057414f,0.003497709f,0.03332392f,0.003475478f,0.03354988f,0.002484241f,0.03336815f,0.000742774f,0.032719396f,-0.0007918071f,0.031931613f,-0.0015234831f,0.031415343f,-0.0018218381f,0.0312858f,
                -0.002152387f,0.03157627f,-0.0023520943f,0.032370668f,-0.0021098808f,0.033533357f,-0.0014036889f,0.034851927f,-0.0007154848f,0.03599856f,-0.0005643939f,0.036584467f,-0.00055373815f,0.036738947f,
                0.00019733547f,0.03696168f,0.0018776387f,0.037480567f,0.0037654932f,0.038059585f,0.0050332365f,0.038344946f,0.005790337f,0.03854964f,0.0069209924f,0.039371297f,0.008736129f,0.040928215f,0.010614511f,
                0.04249687f,0.01200706f,0.043495104f,0.01301777f,0.04416552f,0.013940392f,0.045065988f,0.014877988f,0.04622395f,0.016108626f,0.047269616f,0.017821737f,0.04785953f,0.01948435f,0.04777047f,0.02059024f,
                0.047184333f,0.021447312f,0.046523664f,0.022732442f,0.046034385f,0.024592102f,0.04570177f,0.026480313f,0.045326672f,0.028070614f,0.04483591f,0.029486535f,0.044237964f,0.03064078f,0.04345859f,0.031088732f,
                0.042469107f,0.030467574f,0.04118418f,0.028928999f,0.039348193f,0.026930459f,0.03665575f,0.024845544f,0.03314702f,0.022862643f,0.029413266f,0.02080614f,0.02602395f,0.018353978f,0.023058211f,0.015691353f,
                0.020428969f,0.013382176f,0.018171202f,0.011470385f,0.016190473f,0.009390253f,0.01414171f,0.006858466f,0.011718324f,0.0042997273f,0.0089390455f,0.0022533988f,0.006143218f,0.00080712617f,0.0037662364f,
                -0.00018134009f,0.0021052123f,-0.00084742904f,0.0010900428f,-0.0014482858f,0.00033550296f,-0.0022236346f,-0.0004231848f,-0.002937416f,-0.0009773285f,-0.0031646239f,-0.0010156448f,-0.0031643729f,
                -0.0007825897f,-0.0034881278f,-0.0006765725f,-0.0040549724f,-0.00062209007f,-0.004424028f,-0.00037973392f,-0.004612865f,-6.516307E-05f,-0.005142599f,-8.684518E-05f,-0.006146976f,-0.0004529861f,
                -0.007248811f,-0.0007363678f,-0.008301809f,-0.0007782759f,-0.009388732f,-0.0008086002f,-0.0102694575f,-0.0009639583f,-0.010295365f,-0.00088115275f,-0.0091902455f,-0.00013544777f,-0.0073745316f,
                0.0012570415f,-0.005419166f,0.0029949583f,-0.0037734234f,0.0047070663f,-0.0026266836f,0.00614533f,-0.0018818834f,0.0070455135f,-0.001188742f,0.0071690315f,-0.0003373823f,0.0065678535f,
                0.00032419665f,0.005570353f,0.00045096577f,0.004682895f,0.00012779507f,0.0040569995f,-0.00016917931f,0.0035079177f,-9.7092794E-05f,0.0028892849f,8.9939014E-05f,0.0020702477f,0.00015390827f,
                0.0011003773f,1.860506E-05f,-0.00011047494f,-0.0004317943f,-0.0018433257f,-0.0012633427f,-0.0041722897f,-0.0024966074f,-0.006949836f,-0.003927846f,-0.009854733f,-0.005320777f,-0.012715406f,
                -0.006641429f,-0.015530903f,-0.0080568185f,-0.018300425f,-0.009693817f,-0.020904489f,-0.011416165f,-0.023173863f,-0.013178018f,-0.025284074f,-0.015032582f,-0.027384005f,-0.016965013f,
                -0.029371606f,-0.018813148f,-0.031135272f,-0.020428283f,-0.032825053f,-0.02209526f,-0.034921378f,-0.024193823f,-0.037492514f,-0.026789762f,-0.040173605f,-0.029440418f,-0.042579386f,-0.031609338f,
                -0.04462017f,-0.033430886f,-0.046689365f,-0.035188697f,-0.048865028f,-0.036685072f,-0.050689835f,-0.03737164f,-0.05153525f,-0.037078694f,-0.051143747f,-0.03637597f,-0.05004226f,-0.035592217f,
                -0.048818562f,-0.034437474f,-0.04771957f,-0.032644443f,-0.046735927f,-0.030507728f,-0.04578507f,-0.028636385f,-0.044836093f,-0.02713083f,-0.043674212f,-0.025688495f,-0.042195346f,-0.024119414f,-0.040560883f,
                -0.022414993f,-0.038897537f,-0.020680532f,-0.037144713f,-0.018972645f,-0.035091266f,-0.017116074f,-0.03252193f,-0.014858002f,-0.029330462f,-0.012485743f,-0.025858229f,-0.010784552f,-0.022853428f,-0.010015788f,
                -0.020775912f,-0.009368341f,-0.019289427f,-0.007916835f,-0.01774501f,-0.005837495f,-0.016118435f,-0.0039843805f,-0.0148167135f,-0.0026796004f,-0.013840284f,-0.0014347717f,-0.012571643f,0.00014525896f,
                -0.01056438f,0.0018576705f,-0.008155385f,0.0035857419f,-0.0058010654f,0.005332011f,-0.0036714068f,0.006771491f,-0.0017918824f,0.007581517f,-7.712329E-05f,0.007903071f,0.0015311163f,0.008151789f,0.0029698098f,
                0.008366983f,0.0039296355f,0.008338387f,0.004181833f,0.008153017f,0.003950974f,0.007984741f,0.003544635f,0.007819125f,0.0031562662f,0.0075780703f,0.002956654f,0.007387722f,0.0030248933f,0.0076156533f,0.0033160765f,
                0.008387161f,0.0036590472f,0.0093665505f,0.0040124f,0.010076432f,0.0045534153f,0.01016822f,0.005141156f,0.009719676f,0.0053356793f,0.009144579f,0.0049646343f,0.00867964f,0.0043032514f,0.008239757f,0.003743489f,
                0.007655077f,0.0032332656f,0.0071271746f,0.0026397523f,0.006881362f,0.0021061066f,0.00651264f,0.0016856199f,0.0056292154f,0.0013676231f,0.0044264053f,0.0009274625f,0.0034070385f,6.3035055E-05f,0.0027300382f,
                -0.0012803073f,0.0021716477f,-0.0028867587f,0.0019359069f,-0.004127456f,0.0022509173f,-0.004750705f,0.0027825667f,-0.005151025f,0.0032563068f,-0.005510015f,0.0038769448f,-0.0056537613f,0.0049788803f,-0.0054457737f,
                0.0062570237f,-0.0052145924f,0.007001781f,-0.005334925f,0.0070879925f,-0.0056114597f,0.0069181486f,-0.005777016f,0.006655302f,-0.0060661165f,0.0060755885f,-0.0068955687f,0.0051309066f,-0.0081224255f,0.0039102742f,
                -0.009270896f,0.0022725132f,-0.010176846f,7.372652E-05f,-0.010994119f,-0.0024575328f,-0.011892529f,-0.0047229137f,-0.012720447f,-0.0063592168f,-0.013302083f,-0.0074044005f,-0.013628297f,-0.008019437f,-0.013694158f,
                -0.008582919f,-0.013698615f,-0.009456947f,-0.013859312f,-0.010632689f,-0.014213571f,-0.011778343f,-0.014790089f,-0.012604724f,-0.01563516f,-0.01328692f,-0.016780674f,-0.0142764095f,-0.018197967f,-0.015619486f,
                -0.019637767f,-0.01679519f,-0.020610452f,-0.017445521f,-0.020860728f,-0.017744794f,-0.020606248f,-0.017941017f,-0.020248514f,-0.018018013f,-0.02010712f,-0.017808896f,-0.020219803f,-0.01747863f,-0.020534618f,
                -0.017637186f,-0.021185003f,-0.018616043f,-0.022204023f,-0.020013155f,-0.023302214f,-0.021241322f,-0.024252994f,-0.02210193f,-0.025083514f,-0.022642419f,-0.025725435f,-0.022893393f,-0.025958832f,-0.022884898f,
                -0.025686087f,-0.022736713f,-0.025078928f,-0.022409916f,-0.024327368f,-0.021451097f,-0.023348518f,-0.0194768f,-0.022081636f,-0.01685491f,-0.020839455f,-0.014569611f,-0.02012816f,-0.013194324f,-0.020043597f,
                -0.012411627f,-0.020219099f,-0.011732811f,-0.020445747f,-0.011132013f,-0.020843381f,-0.010949541f,-0.021699784f,-0.011200657f,-0.023030678f,-0.011367129f,-0.024338607f,-0.011146297f,-0.02517345f,-0.010670638f,
                -0.025408112f,-0.010037745f,-0.025125254f,-0.009108199f,-0.024558613f,-0.007734275f,-0.02393514f,-0.006058175f,-0.02335641f,-0.004498744f,-0.022817833f,-0.0034663754f,-0.022306308f,-0.0030323274f,-0.021781191f,
                -0.002893844f,-0.02114858f,-0.0026419978f,-0.020304477f,-0.0021137968f,-0.019251471f,-0.001465003f,-0.018142374f,-0.00066454906f,-0.016882448f,0.0007125293f,-0.014978804f,0.0027826698f,-0.012218084f,0.00484956f,
                -0.0091903135f,0.0061446643f,-0.006674432f,0.0068129194f,-0.0047272737f,0.007672649f,-0.0027939444f,0.009164954f,-0.0005652716f,0.010922779f,0.0016449542f,0.012444064f,0.0035689194f,0.013636427f,0.0054585226f,
                0.014706236f,0.007742543f,0.016046863f,0.010645472f,0.017979473f,0.013950098f,0.020516723f,0.017331079f,0.023243293f,0.020670654f,0.025606686f,0.023958944f,0.027705677f,0.027417298f,0.030028684f,0.03108325f,
                0.03271672f,0.03463407f,0.035544846f,0.03785723f,0.038072575f,0.040531754f,0.040142544f,0.042597286f,0.042040683f,0.04439165f,0.043967646f,0.046221968f,0.04589291f,0.048078157f,0.04770125f,0.049572002f,0.04937206f,
                0.050357282f,0.050993837f,0.05068635f,0.05242027f,0.051072787f,0.053216845f,0.051565573f,0.05320186f,0.05179466f,0.052743014f,0.0515873f,0.052172557f,0.051017515f,0.05139703f,0.05016193f,0.05031295f,0.04907597f,
                0.049137227f,0.047773287f,0.048278473f,0.046422727f,0.047777273f,0.04522541f,0.047153518f,0.044093177f,0.046141773f,0.042946562f,0.044803567f,0.04169975f,0.043048847f,0.040013753f,0.040910967f,0.037753493f,0.03882908f,
                0.03525319f,0.03716199f,0.032965817f,0.03569488f,0.031062875f,0.033963025f,0.029383576f,0.032024376f,0.027830418f,0.030420046f,0.02650973f,0.029442886f,0.025503688f,0.028810425f,0.024761634f,0.028023385f,0.02419637f,
                0.02699355f,0.023936741f,0.026011443f,0.024098124f,0.025219986f,0.024359258f,0.024550984f,0.024291554f,0.023903158f,0.023919187f,0.023170387f,0.023758413f,0.02209043f,0.024093637f,0.020404626f,0.024508368f,0.01840362f,
                0.024584262f,0.016742337f,0.024412578f,0.015755532f,0.02428767f,0.015334073f,0.024352223f,0.015250018f,0.024561465f,0.015326123f,0.024839414f,0.015360443f,0.025142662f,0.015202195f,0.02545067f,0.0148951085f,
                0.025704892f,0.014617471f,0.025847925f,0.014292936f,0.025719587f,0.013570486f,0.025101332f,0.012489784f,0.024246603f,0.011341053f,0.023587674f,0.010097631f,0.023195626f,0.008609948f,0.022945264f,0.0069903727f,
                0.022677183f,0.0056279534f,0.022349674f,0.0045427997f,0.021805853f,0.003263034f,0.020800753f,0.001526924f,0.019465473f,-0.00069439656f,0.017977174f,-0.0034297172f,0.016285535f,-0.0066415383f,0.01430542f,
                -0.010118743f,0.012091511f,-0.013558728f,0.009831954f,-0.016882045f,0.0076472247f,-0.020227626f,0.005667275f,-0.023770448f,0.003949629f,-0.027371248f,0.002441649f,-0.030404767f,0.0012063798f,-0.032376703f,
                0.00031204417f,-0.03345007f,-0.00025992276f,-0.034361165f,-0.00076338125f,-0.035701003f,-0.0015507458f,-0.03720113f,-0.0024666348f,-0.03832185f,-0.0030854486f,-0.03917036f,-0.0034086946f,-0.040241543f,-0.0038426754f,
                -0.04161777f,-0.004681701f,-0.042772904f,-0.005661438f,-0.043225054f,-0.0062150178f,-0.04308472f,-0.0061485027f,-0.042652715f,-0.005646314f,-0.041921757f,-0.0048851958f,-0.040752698f,-0.0039744824f,-0.03917136f,
                -0.0030029549f,-0.03744357f,-0.0020641144f,-0.03597865f,-0.0012434877f,-0.034940787f,-0.0004289996f,-0.03402468f,0.0006132473f,-0.03294877f,0.0017265866f,-0.03183427f,0.0024548469f,-0.030802038f,0.0027569265f
            },
            chordData = string.Empty
        };
        /// <summary>
        /// Constructor
        /// <para>Initialises all asynchronous events.</para>
        /// </summary>
        public pgNetworkTesting()
        {
            InitializeComponent();

            btnPing.Clicked += async (s, e) =>
            {
                await Ping();
            };

            btnRemotePing.Clicked += async (s, e) =>
            {
                await RemotePing();
            };

            btnGet.Clicked += async (s, e) =>
            {
                await GetRequest();
            };

            btnPost.Clicked += async (s, e) =>
            {
                await PostRequest();
            };
        }
        /// <summary>
        /// Uses the <see cref="exampleData"/> to perform a test post-request.
        /// </summary>
        /// <returns>Task, so the method can be run asynchronously.</returns>
        private async Task PostRequest()
        {
            string address = "https://guitarlearningapi.azurewebsites.net/api/Essentia";
            editorOutput = string.Empty;
            editorOutput += "Communication with: " + address + "\n";
            editorOutput += "Sending Post-Request...\n";

            try
            {
                using (HttpRequestMessage request = new HttpRequestMessage())
                {
                    request.RequestUri = new Uri(address);
                    request.Method = HttpMethod.Post;
                    request.Headers.Add("Accept", "application/json");
                    var requestContent = JsonConvert.SerializeObject(exampleData);
                    request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
                    Logger.Log("Request - " + request);
                    Logger.Log("Content - " + request.Content);
                    Logger.Log("Model - " + exampleData.audioData.Length + " - " + exampleData.audioData.ToString() + " - " + exampleData.audioData);

                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.Timeout = new TimeSpan(0, 0, 0, 5);
                        HttpResponseMessage response = await httpClient.SendAsync(request);
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            editorOutput += "Status: OK\n";
                            HttpContent content = response.Content;
                            string json = await content.ReadAsStringAsync();
                            var responseContent = JsonConvert.DeserializeObject<EssentiaModel>(json);
                            editorOutput += "Chords:\n" + responseContent.chordData;
                            Logger.Log("Response - " + "Status: " + response.StatusCode + " Content" + responseContent.chordData);
                        }
                        else
                        {
                            editorOutput += "Status: " + response.StatusCode.ToString() + "\n";
                            Logger.Log("Response - " + "Status: " + response.StatusCode);
                        }
                    }
                }
            }
            catch
            {
                editorOutput += "Timeout\n";
                editorOutput += "Could't reach server\n";
            }
        }
        /// <summary>
        /// Performs a test get-request.
        /// </summary>
        /// <returns>Task, so the method can be run asynchronously.</returns>
        private async Task GetRequest()
        {
            string address = "https://guitarlearningapi.azurewebsites.net/api/Essentia";
            editorOutput = string.Empty;
            editorOutput += "Communication with: " + address + "\n";
            editorOutput += "Sending Get-Request...\n";

            try
            {
                using (HttpRequestMessage request = new HttpRequestMessage())
                {
                    request.RequestUri = new Uri(address);
                    request.Method = HttpMethod.Get;
                    request.Headers.Add("Accept", "application/json");
                    Logger.Log("Request - " + request);
                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.Timeout = new TimeSpan(0, 0, 0, 5);
                        HttpResponseMessage response = await httpClient.SendAsync(request);
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            editorOutput += "Status: OK\n";
                            HttpContent content = response.Content;
                            string json = await content.ReadAsStringAsync();
                            editorOutput += "Content:\n" + json;
                            Logger.Log("Response - " + "Status: " + response.StatusCode + " Content: " + json);
                        }
                        else
                        {
                            editorOutput += "Status: " + response.StatusCode.ToString() + "\n";
                            Logger.Log("Response - " + "Status: " + response.StatusCode);
                        }
                    }
                }
            }
            catch
            {
                editorOutput += "Timeout\n";
                editorOutput += "Could't reach server\n";
            }
        }
        /// <summary>
        /// Reads the url from the entry and tries to ping it (Can only ping remote addresses).
        /// </summary>
        /// <returns>Task, so the method can be run asynchronously.</returns>
        private async Task RemotePing()
        {
            string address = etyURl.Text;

            editorOutput = string.Empty;
            editorOutput += "Pinging " + address + "...\n";

            try
            {
                bool result = await CrossConnectivity.Current.IsRemoteReachable(address);
                if (result)
                {
                    editorOutput += address + " is reachable\n";
                }
                else
                {
                    editorOutput += address + " cannot be reached\n";
                }
            }
            catch
            {
                editorOutput += "Error during ping\n";
                editorOutput += "Please make sure the address is valid\n";
            }
        }
        /// <summary>
        /// Reads the IP from the entry and tries to ping it (Can only ping local addresses).
        /// </summary>
        /// <returns>Task, so the method can be performed asynchronously.</returns>
        private async Task Ping()
        {
            string address = etyURl.Text;

            editorOutput = string.Empty;
            editorOutput += "Pinging " + address + "...\n";
            try
            {
                bool result = await CrossConnectivity.Current.IsReachable(address);
                if (result)
                {
                    editorOutput += address + " is reachable\n";
                }
                else
                {
                    editorOutput += address + " cannot be reached\n";
                }
            }
            catch
            {
                editorOutput += "Error during ping\n";
                editorOutput += "Please make sure the address is valid\n";
            }
        }

        /// <summary>
        /// Bindable property, is used to automatically update the bond output control.
        /// </summary>
        public static readonly BindableProperty editorOutputProperty =
            BindableProperty.Create("editorOutput", typeof(string), typeof(pgNetworkTesting));
        /// <summary>
        /// Getter/Setter for the <see cref="editorOutputProperty"/>.
        /// </summary>
        /// <value>String which is shown in the output control.</value>
        public string editorOutput
        {
            get { return (string)GetValue(editorOutputProperty); }
            set { SetValue(editorOutputProperty, value); }
        }
        /// <summary>
        /// This method is invoked by a button click. It determines whether the dive is connected to any internet connection or not.
        /// </summary>
        /// <param name="sender">Button that invoked the event.</param>
        /// <param name="e">Eventarguments</param>
        private void DoCheckConnectivity(object sender, EventArgs e)
        {
            editorOutput = string.Empty;
            editorOutput += "Checking Connectivity...\n";
            if (CrossConnectivity.Current.IsConnected)
            {
                editorOutput += "Device is connected\n";
            }
            else
            {
                editorOutput += "Device has no connection\n";
            }
        }
        /// <summary>
        /// This method is invoked by a button click. It creates a test-log-file
        /// </summary>
        /// <param name="sender">Button that invoked the event.</param>
        /// <param name="e">Eventarguments</param>
        private void OnCreateLog(object sender, EventArgs e)
        {
            string address = etyURl.Text;

            editorOutput = string.Empty;
            editorOutput += "Creating Test-Log...\n";
            Logger.Log("Test Entry: " + address);
            editorOutput += "Done\n";
        }
    }
}