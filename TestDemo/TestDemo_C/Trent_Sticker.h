
/*************************************************************************
Copyright:   Trent.
Author:		 Trent
Date:		 2015-4-23
Mail:        dongtingyueh@qq.com
Description: Sticker for face
*************************************************************************/
#ifndef __TRENT_STICKER__
#define __TRENT_STICKER__


#ifdef _MSC_VER

#ifdef __cplusplus
#define EXPORT extern "C" _declspec(dllexport)
#else
#define EXPORT __declspec(dllexport)
#endif


EXPORT int Trent_Sticker(unsigned char* srcData, int width, int height, int stride, unsigned char* mask, int maskWidth, int maskHeight, int maskStride, int srcFacePoints[6], int maskFacePoints[6], int ratio);

#else

#ifdef __cplusplus
extern "C" {
#endif    

	int Trent_Sticker(unsigned char* srcData, int width, int height, int stride, unsigned char* mask, int maskWidth, int maskHeight, int maskStride, int srcFacePoints[6], int maskFacePoints[6], int ratio);

#endif
#endif
#pragma once
