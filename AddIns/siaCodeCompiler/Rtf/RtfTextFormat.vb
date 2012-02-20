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
' * Class:		RtfTextFormat
' * Description:	Representa un formato de texto.
' * *****************************************************************************


Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing

Namespace Net.Sgoliver.NRtfTree
	Namespace Util
		''' <summary>
		''' Representa un formato de texto.
		''' </summary>
		Public Class RtfTextFormat
			''' <summary>
			''' Negrita.
			''' </summary>
			Public bold As Boolean = False

			''' <summary>
			''' Cursiva.
			''' </summary>
			Public italic As Boolean = False

			''' <summary>
			''' Subrayado.
			''' </summary>
			Public underline As Boolean = False

			''' <summary>
			''' Nombre de la fuente.
			''' </summary>
			Public font As String = "Arial"

			''' <summary>
			''' Tama�o de la fuente.
			''' </summary>
			Public size As Integer = 10

			''' <summary>
			''' Color de la fuente.
			''' </summary>
			Public color As Color = Color.Black
		End Class
	End Namespace
End Namespace
