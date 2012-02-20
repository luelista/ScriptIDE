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
' * Class:		RtfToken
' * Description:	Token leido por el analizador l�xico para documentos RTF.
' * *****************************************************************************



Namespace Net.Sgoliver.NRtfTree
	Namespace Core
		''' <summary>
		''' Token leido por el analizador l�xico para documentos RTF.
		''' </summary>
		Public Class RtfToken
			#Region "Atributos P�blicos"

			''' <summary>
			''' Tipo del token.
			''' </summary>
			Private m_type As RtfTokenType
			''' <summary>
			''' Palabra clave / S�mbolo de Control / Caracter.
			''' </summary>
			Private m_key As String
			''' <summary>
			''' Indica si el token tiene par�metro asociado.
			''' </summary>
			Private hasParam As Boolean
			''' <summary>
			''' Par�metro de la palabra clave o s�mbolo de Control.
			''' </summary>
			Private param As Integer

			#End Region

			#Region "Propiedades"

			''' <summary>
			''' Tipo del token.
			''' </summary>
			Public Property Type() As RtfTokenType
				Get
					Return m_type
				End Get
				Set
					m_type = value
				End Set
			End Property

			''' <summary>
			''' Palabra clave / S�mbolo de Control / Caracter.
			''' </summary>
			Public Property Key() As String
				Get
					Return m_key
				End Get
				Set
					m_key = value
				End Set
			End Property

			''' <summary>
			''' Indica si el token tiene par�metro asociado.
			''' </summary>
			Public Property HasParameter() As Boolean
				Get
					Return hasParam
				End Get
				Set
					hasParam = value
				End Set
			End Property

			''' <summary>
			''' Par�metro de la palabra clave o s�mbolo de Control.
			''' </summary>
			Public Property Parameter() As Integer
				Get
					Return param
				End Get
				Set
					param = value
				End Set
			End Property

			#End Region

			#Region "Constructor P�blico"

			''' <summary>
			''' Constructor de la clase RtfToken. Crea un token vac�o.
			''' </summary>
			Public Sub New()
				m_type = RtfTokenType.None

					' Inicializados por defecto 

					'hasParam = false;
					'param = 0;
				m_key = ""
			End Sub

			#End Region
		End Class
	End Namespace
End Namespace
