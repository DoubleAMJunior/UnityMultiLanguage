//created by AliAbbasiMoghaddam 2020
using System.Collections.Generic;
using UnityEngine;

public class JsonToDictionary
{
    public JsonToDictionary(TextAsset JsonFile,out Dictionary<string,string> rtn)
    {
        Dictionary<string, string> data=new Dictionary<string, string>();
        //set text for getNextToken function
        targetString = JsonFile.text +"$$";
        //set the pointer of the getNextFunction 
        seeker=0;
        //checks the start of the json
        if (getNextToken().Equals("{"))
        {
            //hold the current token too decide what it will be
            string currentToken=getNextToken();
            //decides if the next income string value will be a key or value
            bool readingKey = true;
            string key = "";
            string value="";
            while (! currentToken.Equals("}"))
            {
                if (currentToken.Equals(","))
                {
                    data.Add(key,value);
                    readingKey = true;
                }
                else if (currentToken.Equals(":"))
                {
                    readingKey = false;
                }

                else if (currentToken.Equals("$"))
                {
                    break;
                }
                else
                {
                    //if current token is none of the above then its a string token
                    if (readingKey)
                        key = currentToken;
                    else
                        value = currentToken;
                }
                currentToken = getNextToken();
            }
        }
        else
        {
            Debug.LogError("LanguageManager:input Json File has invalid format");
            rtn = null;
            return;
        }
        rtn = data;
    }

    private int seeker = 0;
    private string targetString;
    
    private string getNextToken()
    {
        //skips the white space
        if (targetString[seeker]=='\r'||targetString[seeker]==' ' || targetString[seeker]=='\n' || targetString[seeker]=='\t')
        {
            seeker++;
            while (targetString[seeker]=='\r' || targetString[seeker]==' ' || targetString[seeker]=='\n' || targetString[seeker]=='\t')
            {
                seeker++;
            }
            return getNextToken();
        }
        
        if (targetString[seeker]=='{')
        {
            seeker++;//912 767 01 64
            return "{";
        }

        if (targetString[seeker]=='}')
        {
            seeker++;
            return "}";
        }

        if (targetString[seeker]==',')
        {
            seeker++;
            return ",";
        }

        if (targetString[seeker]==':')
        {
            seeker++;
            return ":";
        }

        //detects token end token 
        if (targetString[seeker]=='$' && targetString[seeker+1]=='$')
        {
            return "$";
        }

        
        //detects a key or value token
        if (targetString[seeker]=='"')
        {
            seeker++;
            string val="";
            while (targetString[seeker]!='"' && targetString[seeker-1]!='\\')
            {
                val += targetString[seeker];
                seeker++;
                if (seeker>=targetString.Length)
                {
                    Debug.LogError("LanguageManager:input Json File has invalid format");
                    return "$";
                }
            }
            seeker++;
            return val;
        }
        
        //detects key or value token
        if (targetString[seeker]=='\'')
        {
            seeker++;
            string val="";
            while (targetString[seeker]!='\'' && targetString[seeker-1]!='\\')
            {
                val += targetString[seeker];
                seeker++;
                if (seeker>=targetString.Length)
                {
                    Debug.LogError("LanguageManager:input Json File has invalid format");
                    return "$";
                }
            }
            seeker++;
            return val;
        }
        
        //detects a problem in the input file formatting
        Debug.LogError("LanguageManager:input Json File has invalid format");
        return "$";
    }

}
