﻿using System.Collections.Generic;
#if CONTRACTS_FULL_SHIM
using Contract = System.Diagnostics.ContractsShim.Contract;
#else
using Contract = System.Diagnostics.Contracts.Contract; // SHIM'D
#endif

namespace KSoft.Blam.RuntimeData.Megalo.Proto
{
	[Engine.EngineSystem]
	public sealed class MegaloProtoSystem
		: Engine.EngineSystemBase
	{
		#region SystemGuid
		static readonly Values.KGuid kSystemGuid = new Values.KGuid("F3047D03-8474-44C6-BB9E-49745454BD3D");
		public static Values.KGuid SystemGuid { get { return kSystemGuid; } }
		#endregion

		// NOTE: there should only ever actually be 1 or 2 entries (beta and release)
		Dictionary<Engine.EngineBuildHandle, BuildProtoFiles> mBuildProtoFiles;

		internal MegaloProtoSystem()
		{
			mBuildProtoFiles = new Dictionary<Engine.EngineBuildHandle, BuildProtoFiles>();
		}

		#region ITagElementStreamable<string> Members
		protected override void SerializeExternBody<TDoc, TCursor>(IO.TagElementStream<TDoc, TCursor, string> s)
		{
			using (s.EnterCursorBookmark("BuildFiles"))
			{
				s.StreamableElements("Files",
					mBuildProtoFiles, this.Engine.RootBuildHandle,
					Blam.Engine.EngineBuildHandle.SerializeWithBaseline);
			}
		}
		#endregion
	};
}