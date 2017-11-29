using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCL.COMPGV07.Group8 {
	public static class CustomLogger {
		public static void LogKeyDown()
		{
			try {
				UCL.COMPGV07.Logging.KeyDown();
			} catch {
				Debug.LogWarning("Input logging failed");
			}
		}
	}
}