using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using silkymf = global::silky.MessageFormat;
using silkylf = global::silky.LiteFormat;

namespace StudyTable
{
	#region Static tables
	public partial class Tables
	{
		public static string BasePath = "Table/";

		public delegate byte[] ReadBytesDelegate(string filename);

		public static ReadBytesDelegate BytesReader = (string filename) => {
			#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_IOS || UNITY_ANDROID
			var data = UnityEngine.Resources.Load(filename) as UnityEngine.TextAsset;
			return data.bytes;
			#else
			return System.IO.File.ReadAllBytes($"{ filename}.table");
			#endif
		};

		private static CharacterTable Character__ = null;
		public static CharacterTable Character
		{
			get
			{
				if (Character__ == null)
				{
					ReadAllTables(false);
				}
				return Character__;
			}
		}
		private static UIPrefabTable UIPrefab__ = null;
		public static UIPrefabTable UIPrefab
		{
			get
			{
				if (UIPrefab__ == null)
				{
					ReadAllTables(false);
				}
				return UIPrefab__;
			}
		}

		private static bool areTablesReaded = false;
		public static void ReadAllTables(bool forceReload = false)
		{
			if (!areTablesReaded || forceReload)
			{
				areTablesReaded = true;

				Character__ = new CharacterTable();
				Character__.Read($"{BasePath}Character");
				UIPrefab__ = new UIPrefabTable();
				UIPrefab__.Read($"{BasePath}UIPrefab");
			}

			SolveCrossReferences();
		}

		private static void SolveCrossReferences()
		{
		}
	}
	#endregion // Static tables

	#region CharacterTable
	[System.Serializable]
	public partial class CharacterTable
	{
		#region Record
		[System.Serializable]
		public partial class Record
		{
			#region Properties
			/// <summary>
			/// 크리쳐 이름
			/// </summary>
			public int Id => __Id__;
			private int __Id__;

			public string Name => __Name__;
			private string __Name__;

			public string AgentName => __AgentName__;
			private string __AgentName__;

			public string Nation => __Nation__;
			private string __Nation__;

			public string Description => __Description__;
			private string __Description__;

			public string ModelPath => __ModelPath__;
			private string __ModelPath__;

			public string PrefabPath => __PrefabPath__;
			private string __PrefabPath__;

			public string DatabasePath => __DatabasePath__;
			private string __DatabasePath__;

			public string Portrait => __Portrait__;
			private string __Portrait__;

			public int Hp => __Hp__;
			private int __Hp__;

			public int RecoverHp => __RecoverHp__;
			private int __RecoverHp__;

			public int Power => __Power__;
			private int __Power__;

			/// <summary>
			/// 고통값
			/// </summary>
			public int Pain => __Pain__;
			private int __Pain__;

			/// <summary>
			/// 인내값
			/// </summary>
			public int Patient => __Patient__;
			private int __Patient__;

			public int Speed => __Speed__;
			private int __Speed__;
			#endregion // Properties

			#region Read
			public void Read(silky.IMessageIn input)
			{
				silkylf.Read(input, out __Id__);
				silkylf.Read(input, out __Name__);
				silkylf.Read(input, out __AgentName__);
				silkylf.Read(input, out __Nation__);
				silkylf.Read(input, out __Description__);
				silkylf.Read(input, out __ModelPath__);
				silkylf.Read(input, out __PrefabPath__);
				silkylf.Read(input, out __DatabasePath__);
				silkylf.Read(input, out __Portrait__);
				silkylf.Read(input, out __Hp__);
				silkylf.Read(input, out __RecoverHp__);
				silkylf.Read(input, out __Power__);
				silkylf.Read(input, out __Pain__);
				silkylf.Read(input, out __Patient__);
				silkylf.Read(input, out __Speed__);
			}
			#endregion // Read

			#region ToString
			public override string ToString()
			{
				var sb__0001 = new StringBuilder("{");
				// TODO...
				sb__0001.Append("}");
				return sb__0001.ToString();
			}
			#endregion // ToString
		}
		#endregion // Record

		public List<Record> Records => records;
		private List<Record> records = new List<Record>(5);

		#region Indexing by `Id`
		public Dictionary<int, Record> RecordsById => __recordsById__;
		private Dictionary<int, Record> __recordsById__ = new Dictionary<int, Record>(5);

		public Record GetById(int Id)
		{
			var record__0002 = TryGetById(Id);
			if (record__0002 == null)
			{
				throw new Exception("There is no record in table `CharacterTable` that corresponds to column `Id` value " + Id);
			}

			return record__0002;
		}

		public Record TryGetById(int Id)
		{
			if (__recordsById__.Count == 0)
			{
				foreach (var record__0003 in records)
				{
					__recordsById__.Add(record__0003.Id, record__0003);
				}
			}

			Record record__0004 = null;
			__recordsById__.TryGetValue(Id, out record__0004);
			return record__0004;
		}

		public bool ContainsId(int Id)
		{
			return TryGetById(Id) != null;
		}
		#endregion // Indexing by `Id`

		#region Read
		public void Read(string filename)
		{
			var bytes = Tables.BytesReader(filename);
			Read(bytes);
		}

		public void Read(byte[] data)
		{
			Read(new silky.MessageIn(silky.MessageInExternalArgs.SharedFrom(data)));
		}

		public void Read(silky.IMessageIn input)
		{
			records.Clear();
			__recordsById__.Clear();

			int recordCount__0006 = silkylf.ReadCounter(input);
			for (int i__0005 = 0; i__0005 < recordCount__0006; ++i__0005)
			{
				var record__0007 = new Record();
				record__0007.Read(input);
				records.Add(record__0007);
			}
		}
		#endregion // Read

		#region ToString
		public override string ToString()
		{
			var sb__0009 = new StringBuilder("{");
			sb__0009.Append("\"Records\":[");
			int count__0008 = 0;
			foreach (var record__000a in records)
			{
				if (++count__0008 != 1) sb__0009.Append(",");
				sb__0009.Append(record__000a.ToString());
			}
			sb__0009.Append("]");
			sb__0009.Append("}");
			return sb__0009.ToString();
		}
		#endregion // ToString
	}
	#endregion // CharacterTable

	#region UIPrefabTable
	/// <summary>
	/// 기본공격 관련된 애니메이션 타임 및 길이 및 이펙트
	/// </summary>
	[System.Serializable]
	public partial class UIPrefabTable
	{
		#region Record
		[System.Serializable]
		public partial class Record
		{
			#region Properties
			public int Index => __Index__;
			private int __Index__;

			public string Name => __Name__;
			private string __Name__;

			public string Path => __Path__;
			private string __Path__;
			#endregion // Properties

			#region Read
			public void Read(silky.IMessageIn input)
			{
				silkylf.Read(input, out __Index__);
				silkylf.Read(input, out __Name__);
				silkylf.Read(input, out __Path__);
			}
			#endregion // Read

			#region ToString
			public override string ToString()
			{
				var sb__000b = new StringBuilder("{");
				// TODO...
				sb__000b.Append("}");
				return sb__000b.ToString();
			}
			#endregion // ToString
		}
		#endregion // Record

		public List<Record> Records => records;
		private List<Record> records = new List<Record>(5);

		#region Indexing by `Index`
		public Dictionary<int, Record> RecordsByIndex => __recordsByIndex__;
		private Dictionary<int, Record> __recordsByIndex__ = new Dictionary<int, Record>(5);

		public Record GetByIndex(int Index)
		{
			var record__000c = TryGetByIndex(Index);
			if (record__000c == null)
			{
				throw new Exception("There is no record in table `UIPrefabTable` that corresponds to column `Index` value " + Index);
			}

			return record__000c;
		}

		public Record TryGetByIndex(int Index)
		{
			if (__recordsByIndex__.Count == 0)
			{
				foreach (var record__000d in records)
				{
					__recordsByIndex__.Add(record__000d.Index, record__000d);
				}
			}

			Record record__000e = null;
			__recordsByIndex__.TryGetValue(Index, out record__000e);
			return record__000e;
		}

		public bool ContainsIndex(int Index)
		{
			return TryGetByIndex(Index) != null;
		}
		#endregion // Indexing by `Index`

		#region Read
		public void Read(string filename)
		{
			var bytes = Tables.BytesReader(filename);
			Read(bytes);
		}

		public void Read(byte[] data)
		{
			Read(new silky.MessageIn(silky.MessageInExternalArgs.SharedFrom(data)));
		}

		public void Read(silky.IMessageIn input)
		{
			records.Clear();
			__recordsByIndex__.Clear();

			int recordCount__0010 = silkylf.ReadCounter(input);
			for (int i__000f = 0; i__000f < recordCount__0010; ++i__000f)
			{
				var record__0011 = new Record();
				record__0011.Read(input);
				records.Add(record__0011);
			}
		}
		#endregion // Read

		#region ToString
		public override string ToString()
		{
			var sb__0013 = new StringBuilder("{");
			sb__0013.Append("\"Records\":[");
			int count__0012 = 0;
			foreach (var record__0014 in records)
			{
				if (++count__0012 != 1) sb__0013.Append(",");
				sb__0013.Append(record__0014.ToString());
			}
			sb__0013.Append("]");
			sb__0013.Append("}");
			return sb__0013.ToString();
		}
		#endregion // ToString
	}
	#endregion // UIPrefabTable


	//
	// Enmerations
	//

	namespace Enums
	{
		// Generated from C:/JOPD/TableProject/TableProject/DataTable/Table.xlsx : GAME_ENUM : A2
		public enum TEAM
		{
			NONE = 0,

			/// <summary>
			/// 파란팀 전부 챔프포함
			/// </summary>
			BLUE = 8,

			/// <summary>
			/// 빨간팀 전부 챔프포함
			/// </summary>
			RED = 9,

			/// <summary>
			/// 파랑 크리쳐만
			/// </summary>
			BLUECREATURE = 10,

			/// <summary>
			/// 빨강 크리쳐만
			/// </summary>
			REDCREATURE = 11,

			BLUEWEAPON = 12,

			REDWEAPON = 13,

			/// <summary>
			/// 파랑 챔프만
			/// </summary>
			BLUECAHMP = 20,

			/// <summary>
			/// 빨강 챔프만
			/// </summary>
			REDCHAMP = 21,

			ALLCREATURE = 22,

			/// <summary>
			/// 내 크리쳐만
			/// </summary>
			MYCREATURE = 23,

			/// <summary>
			/// 모든 챔프만
			/// </summary>
			ALLCHAMP = 30,

			/// <summary>
			/// 모든것 전부 챔프포함
			/// </summary>
			ALL = 40,
		}

		// Generated from C:/JOPD/TableProject/TableProject/DataTable/Table.xlsx : GAME_ENUM : F2
		public enum SUMMONTYPE
		{
			/// <summary>
			/// None(automatically inserted)
			/// </summary>
			None = 0,

			/// <summary>
			/// 유저
			/// </summary>
			CHARACTER = 1,

			/// <summary>
			/// 크치려
			/// </summary>
			CREATURE = 2,

			/// <summary>
			/// 공성
			/// </summary>
			BUILDING = 3,

			/// <summary>
			/// 지역스펠
			/// </summary>
			RANGESPELL = 4,

			/// <summary>
			/// 크리쳐 타겟 스펠
			/// </summary>
			TARGETSPELL = 5,

			/// <summary>
			/// 챔프 타게 스펠
			/// </summary>
			CHAMPTARGETSPELL = 6,

			/// <summary>
			/// 크리쳐/챔프 타겟 스펠
			/// </summary>
			ALLTARGETSPELL = 7,

			/// <summary>
			/// 환경스펠
			/// </summary>
			ENVIRSPELL = 8,

			/// <summary>
			/// 베이스크리쳐
			/// </summary>
			BASECREATURE = 9,

			/// <summary>
			/// 발사체
			/// </summary>
			PROJECTTILE = 10,
		}

		// Generated from C:/JOPD/TableProject/TableProject/DataTable/Table.xlsx : GAME_ENUM : J2
		public enum ORDERSTAT
		{
			/// <summary>
			/// None(automatically inserted)
			/// </summary>
			None = 0,

			/// <summary>
			/// 대기
			/// </summary>
			WAITING = 1,

			/// <summary>
			/// 공격
			/// </summary>
			ATTACK = 2,

			/// <summary>
			/// 집으로
			/// </summary>
			GOBACK = 3,
		}
	} // end of namespace Enum

} // end of namespace StudyTable
