using System;
using System.Data;
using System.IO;
using Test;
using UnityEditor;
using UnityEngine;
using Vuforia;

namespace Edit
{
    public class BSHEditor: Editor
    {
        public static readonly string path = Path.Combine(Application.dataPath, "Data/TestData.tsv");


        
        /// <summary>
        /// TSV를 읽어온후, PageData를 생성한다.
        /// </summary>
        [MenuItem("BSH/CreateTestPageData")]
        public static void CreateTestPageData()
        {
            DataTable dataTable = ReadTSV(path);
            foreach (DataRow row in dataTable.Rows)
            {
                TestPageDataSO dataSo = ScriptableObject.CreateInstance<TestPageDataSO>();
                var tmpData = new
                {                
                    pageId = row["번호"].ToString(),
                    option = row["선택지 내용"].ToString(),
                    nextPage = row["갈 수 있는 페이지"].ToString(),
                    gift = row["나타나는 선물"].ToString(),
                    ending = row["엔딩"].ToString(),
                    remarks = row["특이사항"].ToString(),
                    text = row["글"].ToString(),
                    imageOrVideo = row["이미지(영상)"].ToString(),
                    sound = row["사운드"].ToString(),
                    
                };
                if (tmpData.pageId == String.Empty)
                {
                    continue;
                }
                
                dataSo.pageId = int.Parse(tmpData.pageId);
                dataSo.option = tmpData.option;
                String[] rawNextPages = tmpData.nextPage.Split('/');
                
                
                if(rawNextPages.Length == 0)
                {
                    dataSo.nextPages = new int[] {-1};
                }
                else
                {
                    dataSo.nextPages =
                        Array.ConvertAll(rawNextPages,
                            e => e == String.Empty ? -1 : int.Parse(e));
                }
                
                dataSo.gift = tmpData.gift;
                dataSo.ending = tmpData.ending;
                dataSo.remarks = tmpData.remarks;
                dataSo.text = tmpData.text;
                dataSo.imageOrVideo = tmpData.imageOrVideo;
                dataSo.sound = tmpData.sound;
                
                Debug.Log("Parsed Data: " + dataSo);

                AssetDatabase.CreateAsset(dataSo, $"Assets/ScriptableObjects/TestPageData{dataSo.pageId:D2}.asset");
            }

            AssetDatabase.SaveAssets();
        }
        
        
        private static DataTable ReadTSV(string path)
        {
            DataTable dataTable = new DataTable();
            string[] lines = System.IO.File.ReadAllLines(path);
            string[] headers = lines[0].Split('\t');
            for (int i = 0; i < headers.Length; i++)
            {
                dataTable.Columns.Add(headers[i]);
            }
            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split('\t');
                DataRow row = dataTable.NewRow();
                for (int j = 0; j < values.Length; j++)
                {
                    row[j] = values[j];
                }
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
    }
}