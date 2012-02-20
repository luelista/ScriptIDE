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
' * Class:		RtfColorTable
' * Description:	Tabla de Colores de un documento RTF.
' * *****************************************************************************


Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing

Namespace Net.Sgoliver.NRtfTree
	Namespace Util
		''' <summary>
		''' Tabla de colores de un documento RTF.
		''' </summary>
		Public Class RtfColorTable
			''' <summary>
			''' Lista interna de colores.
			''' </summary>
			Private colors As List(Of Integer)

			''' <summary>
			''' Constructor de la clase RtfColorTable.
			''' </summary>
			Public Sub New()
				colors = New List(Of Integer)()
			End Sub

			''' <summary>
			''' Inserta un nuevo color en la tabla de colores.
			''' </summary>
			''' <param name="color">Nuevo color a insertar.</param>
			Public Sub AddColor(color As Color)
				colors.Add(color.ToArgb())
			End Sub

			''' <summary>
			''' Obtiene el color n-�simo de la tabla de colores.
			''' </summary>
			''' <param name="index">Indice del color a recuperar.</param>
			''' <returns>Color n-�simo de la tabla de colores.</returns>
			Public Default ReadOnly Property Item(index As Integer) As Color
				Get
					Return Color.FromArgb(colors(index))
				End Get
			End Property

			''' <summary>
			''' N�mero de colores en la tabla.
			''' </summary>
			Public ReadOnly Property Count() As Integer
				Get
					Return colors.Count
				End Get
			End Property

			''' <summary>
			''' Obtiene el �ndice de un color determinado en la tabla.
			''' </summary>
			''' <param name="color">Color a consultar.</param>
			''' <returns>Indice del color consultado.</returns>
			Public Function IndexOf(color As Color) As Integer
				Return colors.IndexOf(color.ToArgb())
			End Function
		End Class
	End Namespace
End Namespace
