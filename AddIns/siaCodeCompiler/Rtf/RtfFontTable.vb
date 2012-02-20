'*******************************************************************************
' *   This file is part of NRtfTree Library.
' *
' *   NRtfTree Library is free software; you can redistribute it and/or modify
' *   it under the terms of the GNU Lesser General Public License as published by
' *   the Free Software Foundation; either version 3 of the License, or
' *   (at your option) any later version.
' *
' *   NRtfTree Library is distributed in the hope that it will be useful,
' *   but WITHOUT ANY WARRANTY; without even the implied warranty of
' *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' *   GNU Lesser General Public License for more details.
' *
' *   You should have received a copy of the GNU Lesser General Public License
' *   along with this program. If not, see <http://www.gnu.org/licenses/>.
' *******************************************************************************


'*******************************************************************************
' * Library:		NRtfTree
' * Version:     v0.3.0
' * Date:		02/09/2007
' * Copyright:   2007 Salvador Gomez
' * E-mail:      sgoliver.net@gmail.com
' * Home Page:	http://www.sgoliver.net
' * SF Project:	http://nrtftree.sourceforge.net
' *				http://sourceforge.net/projects/nrtftree
' * Class:		RtfFontTable
' * Description:	Tabla de Fuentes de un documento RTF.
' * *****************************************************************************


Imports System.Collections.Generic
Imports System.Text

Namespace Net.Sgoliver.NRtfTree
	Namespace Util
		''' <summary>
		''' Tabla de fuentes de un documento RTF.
		''' </summary>
		Public Class RtfFontTable
			''' <summary>
			''' Lista interna de fuentes.
			''' </summary>
			Private fonts As List(Of String)

			''' <summary>
			''' Constructor de la clase RtfFontTable.
			''' </summary>
			Public Sub New()
				fonts = New List(Of String)()
			End Sub

			''' <summary>
			''' Inserta un nueva fuente en la tabla de fuentes.
			''' </summary>
			''' <param name="color">Nueva fuente a insertar.</param>
			Public Sub AddFont(name As String)
				fonts.Add(name)
			End Sub

			''' <summary>
			''' Obtiene la fuente n-�sima de la tabla de fuentes.
			''' </summary>
			''' <param name="index">Indice de la fuente a recuperar.</param>
			''' <returns>Fuente n-�sima de la tabla de fuentes.</returns>
			Public Default ReadOnly Property Item(index As Integer) As String
				Get
					Return fonts(index)
				End Get
			End Property

			''' <summary>
			''' N�mero de fuentes en la tabla.
			''' </summary>
			Public ReadOnly Property Count() As Integer
				Get
					Return fonts.Count
				End Get
			End Property

			''' <summary>
			''' Obtiene el �ndice de una fuente determinado en la tabla.
			''' </summary>
			''' <param name="color">Fuente a consultar.</param>
			''' <returns>Indice de la fuente consultada.</returns>
			Public Function IndexOf(name As String) As Integer
				Return fonts.IndexOf(name)
			End Function
		End Class
	End Namespace
End Namespace
