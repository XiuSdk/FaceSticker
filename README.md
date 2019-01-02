# FaceSticker
Face sticker effects
本代码基于mtcnn五个人脸关键点，实现人脸静态贴纸特效，代码简单，50行左右；
运行平台为WINDOWS VS2015，编译时只需要将model_path路径替换为本地路径即可，model_path在Trent.cpp中；
本代码使用C#做上层界面，调用C封装的DLL，底层使用ncnn调用mtcnn模型，不依赖于任何第三方库，包括Opencv；
