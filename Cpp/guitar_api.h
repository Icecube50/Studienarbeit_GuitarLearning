#ifndef GUITAR_API_H
#define GUITAR_API_H

#ifdef __cplusplus
extern "C" {
#endif

#ifdef BUILDING_GUITAR_DLL
#define GUITAR_DLL __declspec(dllexport)
#else
#define GUITAR_DLL __declspec(dllimport)
#endif

char* __stdcall GUITAR_DLL CalculateChords(float* audioInput, long int audioInputSize/*, long int DEBUG_MODE/*, int sampleRate, int frameSize, int hopSize*/);

void __stdcall GUITAR_DLL HelloWorld();

int GUITAR_DLL Double(int Number);

float GUITAR_DLL TestFloatArray(float* Buffer, int Size);

int GUITAR_DLL TestIntArray(int* Buffer, int Size);

char* __stdcall GUITAR_DLL TestString();

char* __stdcall GUITAR_DLL TestFloatArrayAndString(float* Buffer, int Size);

#ifdef __cplusplus
}
#endif

#endif  // GUITAR_API_H
