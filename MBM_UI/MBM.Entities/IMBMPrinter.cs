using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace MBM.Entities
{
	public interface IMBMPrinter
	{
		OrderedDictionary MBMOutput { get; }
	}
}
