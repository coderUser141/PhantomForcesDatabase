#pragma once

#ifdef DBFEXPORT
#define DBFLIB __declspec(dllexport)
#else
#define DBFLIB __declspec(dllimport)
#endif

