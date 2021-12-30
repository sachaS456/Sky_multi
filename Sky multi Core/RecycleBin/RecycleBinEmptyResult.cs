using System;

namespace Sky_multi_Core
{
	/// <summary>
	/// Fournit les r�sultats d'une op�ration de vidage.
	/// <seealso cref="RecycleBin">RecycleBin Class</seealso>
	/// </summary>
	public class RecycleBinEmptyResult
	{
		/// <summary>
		/// Initialise une nouvelle instance de <see cref="RecycleBinEmptyResult"/>.
		/// </summary>
		/// <param name="ret">Code retourn� par SHEmptyRecycleBin.</param>
		internal RecycleBinEmptyResult( int ret )
		{
			if ( ret == RecycleBinWin32.S_OK )
				m_success = true;
			
			m_OLEErrorCode = ret;
		}

		#region Champs

		private bool m_success;
		private int m_OLEErrorCode;

		#endregion

		#region Propri�t�s

		/// <summary>
		/// Obtient le r�sultat de l'op�ration.
		/// </summary>
		/// <value><c>true</c> est cas de r�ussite, sinon <c>false</c>.</value>
		public bool Success
		{
			get
			{
				return m_success;
			}
		}

		/// <summary>
		/// Obtient le code retourn� par la m�thode SHEmptyRecycleBin.
		/// </summary>
		/// <value>Un "OLE-defined error value" si <c>Success</c> vaut <c>false</c>, sinon 0.</value>
		public int OLEErrorCode
		{
			get
			{
				return m_OLEErrorCode;
			}
		}

		#endregion
	}
}
