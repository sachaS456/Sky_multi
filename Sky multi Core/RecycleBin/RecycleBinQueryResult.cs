/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2022 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-3.0.html. 

--------------------------------------------------------------------------------------------------------------------*/

using System;

namespace Sky_multi_Core
{
	/// <summary>
	/// Fournit les r�sultats d'une demande d'informations.
	/// <seealso cref="RecycleBin">RecycleBin Class</seealso>
	/// </summary>
	public class RecycleBinQueryResult
	{
		/// <summary>
		/// Initialise une nouvelle instance de <see cref="RecycleBinQueryResult"/>.
		/// </summary>
		/// <param name="ret">Code retourn� par SHQueryRecycleBin.</param>
		/// <param name="infos">Structure <see cref="RecycleBinWin32.SHQUERYRBINFO"/> utilis�e avec SHQueryRecycleBin.</param>
		internal RecycleBinQueryResult(int ret, RecycleBinWin32.SHQUERYRBINFO infos)
		{
			if ( ret == RecycleBinWin32.S_OK )
			{
				m_success = true;
				m_rbSize = infos.i64Size;
				m_rbNumItems = infos.i64NumItems;
			}
			
			m_OLEErrorCode = ret;
		}

		#region Champs

		private bool m_success;
		private int m_OLEErrorCode;
		private ulong m_rbSize;
		private ulong m_rbNumItems;
		
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
		/// Obtient le code retourn� par la m�thode SHQueryRecycleBin.
		/// </summary>
		/// <value>Un "OLE-defined error value" si <c>Success</c> vaut <c>false</c>, sinon 0.</value>
		public int OLEErrorCode
		{
			get
			{
				return m_OLEErrorCode;
			}
		}

		/// <summary>
		/// Obtient la taille en octets de la totalit� des �l�ments pr�sents dans la corbeille pour laquelle les informations ont �t� demand�es.
		/// </summary>
		public ulong RBSize
		{
			get
			{
				return m_rbSize;
			}
		}

		/// <summary>
		/// Obtient le nombre total d'�l�ments pr�sents dans la corbeille pour laquelle les informations ont �t� demand�es.
		/// </summary>
		public ulong RBNumItems
		{
			get
			{
				return m_rbNumItems;
			}
		}

		#endregion
	}
}
