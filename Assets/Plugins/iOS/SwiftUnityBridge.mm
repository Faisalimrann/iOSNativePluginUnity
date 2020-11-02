//
//  SwiftUnityBridge.m
//  medialib
//
//  Created by Mac on 27/08/2020.
//  Copyright © 2020 Mac. All rights reserved.
//

#include "UnityFramework/UnityFramework-Swift.h"
#pragma mark - C interface
extern "C" {
     char* _sayHiToUnity() {
          NSString *returnString = [[SwiftPlugin shared]       SayHiToUnity];
          char* cStringCopy(const char* string);
          return cStringCopy([returnString UTF8String]);
     }
void sendMail(){
    [[SwiftPlugin shared]       sendMail];
}
}
char* cStringCopy(const char* string){
     if (string == NULL){
          return NULL;
     }
     char* res = (char*)malloc(strlen(string)+1);
     strcpy(res, string);
     return res;
}

