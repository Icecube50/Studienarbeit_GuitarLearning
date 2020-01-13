#include <stdio.h>
#include "guitar_api.h"
#include "essentia/algorithmfactory.h"
#include "essentia/streaming/algorithms/poolstorage.h"

using namespace std;
using namespace essentia;
using namespace essentia::standard;

char* __stdcall CalculateChords(float* audioInput, long int audioInputSize/*, long int DEBUG_MODE/*, int sampleRate, int frameSize, int hopSize*/){
    // Initializing
    std::vector<Real> audioProcessReady;
    Pool pool;
    char* returnPointer = NULL;

    //Parameters
    const int SAMPLE_RATE = 44100;
    const int FRAME_SIZE = 2048;
    const int HOP_SIZE = 1024;
    const int DEBUG_MODE = 1;

    //Converting float array to vector<Real>
    if(DEBUG_MODE) printf("--- Converting float array ---\n");
    for(int i = 0; i < audioInputSize; i++){
        Real r = audioInput[i];
        audioProcessReady.push_back(r);
    }
    if(DEBUG_MODE) printf("--- Convert Done ---\n");

    essentia::init();   

    AlgorithmFactory& factory = standard::AlgorithmFactory::instance();

    Algorithm* frameCutter = factory.create("FrameCutter",
                                            "frameSize", FRAME_SIZE,
                                            "hopSize", HOP_SIZE);
                                        
    Algorithm* windowing = factory.create("Windowing",
                                        "type", "blackmanharris62");

    Algorithm* spectrum = factory.create("Spectrum");

    Algorithm* peak = factory.create("SpectralPeaks");

    Algorithm* hpcp = factory.create("HPCP");

    Algorithm* chordDetector = factory.create("ChordsDetection");

    // Connecting the algorithms
    if(DEBUG_MODE) printf("--- Start connecting ---\n");
    //Audio -> FrameCutter
    frameCutter->input("signal").set(audioProcessReady);

    //FrameCutter -> Windowing -> Spectrum
    std::vector<Real> frame, windowedFrame;

    frameCutter->output("frame").set(frame);
    windowing->input("frame").set(frame);

    windowing->output("frame").set(windowedFrame);
    spectrum->input("frame").set(windowedFrame);

    //Spectrum -> Peaks -> HPCP
    std::vector<Real> signalSpectrum, signalFrequencies, signalMagnitudes;

    spectrum->output("spectrum").set(signalSpectrum);
    peak->input("spectrum").set(signalSpectrum);

    peak->output("frequencies").set(signalFrequencies);
    peak->output("magnitudes").set(signalMagnitudes);

    hpcp->input("frequencies").set(signalFrequencies);
    hpcp->input("magnitudes").set(signalMagnitudes);

    //HPCP -> Chords
    std::vector<Real> signalHPCP, chordStrength;
    std::vector<string> chordName;

    hpcp->output("hpcp").set(signalHPCP);

    try{
        // Processing the audio
        if(DEBUG_MODE) printf("--- Start processing ---\n");
        while(true){
            frameCutter->compute();

            //if it was the last frame, end the loop
            if(!frame.size()){
                break;
            }

            windowing->compute();
            spectrum->compute();
            peak->compute();
            hpcp->compute();

            if(DEBUG_MODE) printf("--- Adding HPCP to pool ---\n");
            pool.add("HPCP", signalHPCP);
        }

        std::vector<vector<Real>> signalPCP;

        if(DEBUG_MODE){
            printf("--- Getting PCP ---\n");
            signalPCP = pool.value<vector<vector<Real>>>("HPCP");
            printf("--- Getting PCP.size() ---\n");
            int pcpSize = int(signalPCP.size());
            printf("--- Got Size ---\n");
            if(pcpSize > 0) printf("Size is positive\n");
        }

        //Computing
        if(DEBUG_MODE) printf("--- Computing Chords ---\n");
        chordDetector->input("pcp").set(signalPCP);
        chordDetector->output("chords").set(chordName);
        chordDetector->output("strength").set(chordStrength);
        chordDetector->compute();
        
        if(DEBUG_MODE){
            printf("--- Checking Result ---\n");
            int chordSize = chordName.size();
            if(chordSize > 0) printf("At least one chord\n");
            int strengthSize = chordStrength.size();
            if(strengthSize > 0) printf("At least one intensity\n");
            if(chordSize == strengthSize) printf("Intensity for every chord\n");
        }

        //Cleanup
        if(DEBUG_MODE) printf("--- Do Cleanup ---\n");
        delete frameCutter;
        delete windowing;
        delete spectrum;
        delete peak;
        delete hpcp;
        delete chordDetector;

        if(DEBUG_MODE) printf("--- Do Shutdown ---\n");
        essentia::shutdown();

        //Building return string
        if(DEBUG_MODE) printf("--- Build return String ---\n");
        string completeChordString = "";
        if(chordName.size() == chordStrength.size()){
            for(int i = 0; i < chordName.size(); i++){
                if(chordStrength[i] < 0) continue;

                string chordString = "";
                chordString += chordName[i];
                chordString += ",";
                
                //float to string conversion
                ostringstream os;
                os << chordStrength[i];
                string chordStrengthString = os.str();

                chordString += chordStrengthString;
                chordString += ";";

                //Add to result String
                completeChordString += chordString;
            }
        }

        //Converting to CRL-Conform string
        if(DEBUG_MODE) printf("--- Converting to CLR-String ---\n");

        char* cString = new char[completeChordString.size() + 1];

        if(DEBUG_MODE) printf("strcpy(cString, completeChordString.c_string())\n");
        strcpy(cString, completeChordString.c_str());

        ULONG Size = strlen(cString) + sizeof(char);

        returnPointer = (char*)::CoTaskMemAlloc(Size);

        if(DEBUG_MODE) printf("strcpy(returnPointer, cString)\n");
        strcpy(returnPointer, cString);

        delete[] cString;
    }
    catch(...){
        if(DEBUG_MODE) printf("--- Entered Catch ---\n");

        string failed = "-1";
        char* cString = new char[failed.size() + 1];

        if(DEBUG_MODE) printf("strcpy(cString, failed.c_string())\n");
        strcpy(cString, failed.c_str());

        ULONG Size = strlen(cString) + sizeof(char);

        returnPointer = (char*)::CoTaskMemAlloc(Size);

        if(DEBUG_MODE) printf("strcpy(returnPointer, cString)\n");
        strcpy(returnPointer, cString);

        delete[] cString;
    }

    if(DEBUG_MODE){
        printf("--- Returning to C# ---\n");
        printf("%s", returnPointer);
        printf("\n");
    }
    return returnPointer;
}

__stdcall void HelloWorld(){
    printf("Hello World %s\n");
}

int Double(int Number){
    return 2* Number;
}

float TestFloatArray(float* Buffer, int Size){
    float sum = 0.0;
    for(int i = 0; i < Size; i++){
        sum = sum + Buffer[i];
    }
    return sum;
}

char* __stdcall TestFloatArrayAndString(float* Buffer, int Size){
    float sum = 0.0;
    for(int i = 0; i < Size; i++){
        sum = sum + Buffer[i];
    }
    
    string test = "Dies ist ein TestString";
    char* cString = new char[test.size() + 1];
    strcpy(cString, test.c_str());

    ULONG stringSize = strlen(cString) + sizeof(char);
    char* ReturnPointer = NULL;
    ReturnPointer = (char*)::CoTaskMemAlloc(stringSize);
    strcpy(ReturnPointer, cString);

    delete[] cString;
    return ReturnPointer;
}

int TestIntArray(int* Buffer, int Size){
    int sum = 0;
    for(int i = 0; i < Size; i++){
        sum = sum + Buffer[i];
    }
}

char* __stdcall TestString(){
    string test = "Dies ist ein TestString";
    char* cString = new char[test.size() + 1];
    strcpy(cString, test.c_str());

    ULONG Size = strlen(cString) + sizeof(char);
    char* ReturnPointer = NULL;

    printf("MemorySizeString: ");
    printf("%i", Size);
    printf("\n");

    ReturnPointer = (char*)::CoTaskMemAlloc(Size);
    printf("PointerSize: %i", sizeof(ReturnPointer));
    printf("\n");
    printf("PointerString: %i", strlen(ReturnPointer) + 1);
    printf("\n");
    strcpy(ReturnPointer, cString);
    printf("PointerSize After: %i", sizeof(ReturnPointer));
    printf("\n");
    printf("PointerString After: %i", strlen(ReturnPointer) + 1);
    printf("\n");

    return ReturnPointer;
}