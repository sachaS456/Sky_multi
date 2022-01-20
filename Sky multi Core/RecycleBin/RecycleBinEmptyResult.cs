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
	/// Fournit les résultats d'une opération de vidage.
	/// <seealso cref="RecycleBin">RecycleBin Class</seealso>
	/// </summary>
	public class RecycleBinEmptyResult
	{
		/// <summary>
		/// Initialise une nouvelle instance de <see cref="RecycleBinEmptyResult"/>.
		/// </summary>
		/// <param name="ret">Code retourné par SHEmptyRecycleBin.</param>
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

		#region Propriétés

		/// <summary>
		/// Obtient le résultat de l'opération.
		/// </summary>
		/// <value><c>true</c> est cas de réussite, sinon <c>false</c>.</value>
		public bool Success
		{
			get
			{
				return m_success;
			}
		}

		/// <summary>
		/// Obtient le code retourné par la méthode SHEmptyRecycleBin.
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
