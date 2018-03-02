using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using UnityEngine;
using UnityEditor;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Net;
using System;

public class CreatSheet : MonoBehaviour
{
	static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
	static string ApplicationName = "Get Google SheetData with Google Sheets API";

	[MenuItem("Sheet/eventSheet")]
	public static void Creat()
	{
		
		IList<IList<object>> values = GetSheet("1FdXGmYmFHt_nnkBtfbvBPaMpGbOsTGAzUL9ir11BUkk", "test", "A2", "K200");
		string jsonText = "{\"eventSheet\":[\r\n{";

		if (values != null && values.Count > 0)
		{
			for (int i = 0; i < values.Count; i++)
			{
				if (values[i].Count == 9)
				{
					print("事件Json創建完成");
					jsonText += "\"ID\":" + values[i][0] + ",\r\n";
					jsonText += "\"State\":" + values[i][1] +",";
					jsonText += "\"Sort\":" + values[i][2] + ",";
					jsonText += "\"Aims\":\"" + values[i][3] + "\",";
					jsonText += "\"Schedule\":\"" + values[i][4] + "\",";
					jsonText += "\"NpcID\":\"" + values[i][5] + "\",";				
					jsonText += "\"Target\":\"" + values[i][6] +"\",";
					jsonText += "\"Dialog\":\"" + values[i][7]+"\",";
					jsonText += "\"icon\":\"" + values[i][8] + "\"";
				}
				if (i != values.Count - 1)
					jsonText += "}, \r\n {";
				else
					jsonText += "}\r\n]}";
			}
			CreateJsonFile(jsonText, "eventTest");	
		}

	}
	[MenuItem("Sheet/LyricSheet")]
	public static void CreatLyricSheet()
	{
		IList<IList<object>> values = GetSheet("150MzrhndRkSFOlin4vxm_6lMoDEYB2b1SCgwT06kjtY", "test", "A2", "E200");
		string ChageUse,TalkS,SpriteRS, SpriteLS;
		string jsonText = "{\"LyricSheet\":[\r\n{";
		if (values != null && values.Count > 0)
		{
			print("成功創建");
			print(values[0].Count);
			for (int i = 0; i < values.Count; i++)
			{
				if (values[i].Count ==5)
				{
					ChageUse = values[i][1].ToString();
					TalkS = values[i][2].ToString();
					SpriteLS = values[i][3].ToString();
					SpriteRS = values[i][4].ToString();
					jsonText += "\"ID\":" + values[i][0] + ",";
					if (ChageUse.Contains("#"))
					{
						Char[] cC = ChageUse.ToCharArray();
						Char[] tC = TalkS.ToCharArray();
						Char[] sLC = SpriteLS.ToCharArray();
						Char[] sRC = SpriteRS.ToCharArray();
						jsonText += "\"lyric\":\r\n[{\"String\":\"";
						for (int k = 0; k < cC.Length; k++)
						{
							if (!cC[k].ToString().Contains("#")) jsonText += cC[k];
							else
							{
								jsonText += "\",\"TalkWay\":";
								for (int y = 0; y < tC.Length; y++)
								{
									if (TalkS.Contains("#")) TalkS = TalkS.Remove(0, 1);
									if (!tC[y].ToString().Contains("#")) jsonText += tC[y];
									else
									{
										tC = TalkS.ToCharArray();
										jsonText += ",\"LSprite\":\"";
										for (int t = 0; t < sLC.Length; t++)
										{
											if (SpriteLS.Contains("#")) SpriteLS = SpriteLS.Remove(0, 1);
											if (!sLC[t].ToString().Contains("#")) jsonText += sLC[t];
											else
											{
												sLC = SpriteLS.ToCharArray();
												jsonText += "\",\"RSprite\":\"";
												for (int x=0; x < sRC.Length; x++)
												{
													if (SpriteRS.Contains("#")) SpriteRS = SpriteRS.Remove(0, 1);																											
													if (!sRC[x].ToString().Contains("#")) jsonText += sRC[x];
													else
													{
														sRC = SpriteRS.ToCharArray();
														jsonText += "\"}";
														break;
													}
												}
												break;
											}
										}
										break;
									}
								}
								jsonText += ",\r\n{\"String\":\"";
							}
							if (k == cC.Length - 1)
							{
								jsonText += "\",\"TalkWay\":"+ TalkS+ ",\"LSprite\":\""+SpriteLS + "\",\"RSprite\":\""+ SpriteRS+"\"";
							}
						}
					}
					else jsonText += "\"lyric\":[{\"String\":\"" + values[i][1] + "\",\"TalkWay\":"+ values[i][2]+ ",\"LSprite\":\"" + values[i][3]+"\"" + ",\"RSprite\":\"" + values[i][3] + "\"";			
				}
				else print("No");

				if (i != values.Count - 1)
					jsonText += "}]}, \r\n {";
				else
					jsonText += "}]}\r\n]}";
			}
			CreateJsonFile(jsonText, "lyricJson");
		}
	}
	[MenuItem("Sheet/LyricSheetNew")]
	public static void NewCreatLyricSheet()
	{
		IList<IList<object>> values = GetSheet("150MzrhndRkSFOlin4vxm_6lMoDEYB2b1SCgwT06kjtY", "test", "A2", "E200");
		string jsonText ="{\"LyricSheet\":[\r\n{";
		if (values != null && values.Count > 0)
		{
			print("成功創建");
			print(values[2].Count);
			for (int i = 0; i < values.Count; i++)
			{
				if (values[i].Count == 5)
				{
					print("事件Json創建完成");
					jsonText += "\"ID\":" + values[i][0] + ",\r\n";
					jsonText += "\"String\":\"" + values[i][1] + "\",";
					jsonText += "\"TalkWay\":\"" + values[i][2] + "\",";
					jsonText += "\"LSprite\":\"" + values[i][3] + "\",";
					jsonText += "\"RSprite\":\"" + values[i][4] + "\"";
				}
				if (i != values.Count - 1)
					jsonText += "}, \r\n {";
				else
					jsonText += "}\r\n]}";
			}
			CreateJsonFile(jsonText, "lyricJson");
			#region
			/*for (int i = 0; i < values.Count; i++)
			{
				lryic = values[i][1].ToString().Split('#');
				TalkWay = values[i][2].ToString().Split('#');
				SpriteLS = values[i][3].ToString().Split('#');
				SpriteRS = values[i][4].ToString().Split('#');
				if (values[i].Count == 5 && i < values.Count - 1)
				{					
					jsonText += "\"ID\":" + values[i][0] + ",\"lryic\":[{";
					for (int j = 0; j < lryic.Length; j++)
					{
						if (j < lryic.Length - 1)
						{
							jsonText += "\"String\":\"" + lryic[j] + "\",\"TalkWay\":" + TalkWay[j] + ",\"LSprite\":" + "\"" +
							SpriteLS[j] + "\",\"RSprite\":" + "\"" + SpriteRS[j] + "\"},{";
						}
						else
						{
							jsonText += "\"String\":\"" + lryic[j] + "\",\"TalkWay\":" + TalkWay[j] + ",\"LSprite\":" + "\"" +
							SpriteLS[j] + "\",\"RSprite\":" + "\"" + SpriteRS[j] + "\"}]},{";
						}
					}
				}
				else 
				{
					for (int j = 0; j < lryic.Length; j++)
					{
						jsonText += "\"ID\":" + values[i][0] + ",\"lryic\":[{";
						if (j < lryic.Length - 1)
						{
							jsonText += "\"String\":\"" + lryic[j] + "\",\"TalkWay\":" + TalkWay[j] + ",\"LSprite\":" + "\"" +
							SpriteLS[j] + "\",\"RSprite\":" + "\"" + SpriteRS[j] + "\"},{";
						}
						else
						{
							jsonText += "\"String\":\"" + lryic[j] + "\",\"TalkWay\":" + TalkWay[j] + ",\"LSprite\":" + "\"" +
							SpriteLS[j] + "\",\"RSprite\":" + "\"" + SpriteRS[j] + "\"}]}]}";
						}
					}
				}
			}*/
			#endregion

		}
	
	}
	private static IList<IList<object>> GetSheet(string SheetID, string SheetName, string StartAt, string EndAt)
	{
		
		UserCredential credential;
		using (var stream =
				new FileStream("client_secret_json.json", FileMode.Open, FileAccess.Read))
		{
			string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			credPath = Path.Combine(credPath, ".credentials/sheets.googleapis.com-dotnet-quickstart.json");
			credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
				GoogleClientSecrets.Load(stream).Secrets,
				Scopes,
				"user",
				CancellationToken.None,
				new FileDataStore(credPath, true)).Result;
		}
		var service = new SheetsService(new BaseClientService.Initializer()
		{
			HttpClientInitializer = credential,
			ApplicationName = ApplicationName,
		});
		ServicePointManager.ServerCertificateValidationCallback =
					delegate { return true; };
		// Define request parameters.
		string spreadsheetId = SheetID;
		string range = SheetName + "!" + StartAt + ":" + EndAt;
		SpreadsheetsResource.ValuesResource.GetRequest request =
				service.Spreadsheets.Values.Get(spreadsheetId, range);
		ValueRange response = request.Execute();
		IList<IList<object>> values = response.Values;
		return values;
	}
	static void CreateJsonFile(string Content, string FileName)
	{
		string filePath = Application.dataPath;
		Directory.CreateDirectory(filePath);
		StreamWriter sw = new StreamWriter(filePath + "/" + FileName + ".txt", false, Encoding.UTF8);
		sw.Write(Content);
		sw.Close();
		sw.Dispose();
	}
}
