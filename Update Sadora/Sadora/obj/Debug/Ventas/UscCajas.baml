   M S B A M L   `   `   `  ����A  =Sadora, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Sadora.VentasSadora.Ventas  5          Sadora.Ventas.UscCajas   5      a ]DevExpress.Xpf.Grid.v19.1, Version=19.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af bDevExpress.Xpf.Grid.v19.1.Core, Version=19.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v19.1, Version=19.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:Sadora.Ventas     5      b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      -   5        $X��Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)�5      $��Stretch=�5
      $��Stretch=�6,    "  SecundaryAccentForegroundBrush#���   5      
  � Tag$  Configuracion de Cajas��5      .��5      � .+�5   
   �  � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $��* �6    .+�5      � $��0��6   $��15q�6     � ColumnDefinitions 5      �� 5      $��* �6'   �� 5      $��* �6'   �� 5      $��* �6'   �� 5      $��* �6'   �� 5      $��* �6'   �� 5      $��* �6'   �� 5      $��* �6'   �� 5       $��* �6'   �� 5!      $��* �6'   �� 5"      $��* �6'   �� 5#      $��* �6'   �� 5$      $��60 �6'   .+�5'      ��  �� Padding$ 0q�6   $��0��6%     PrimaryHueDarkBrush#��[ 65   $	��1.5q�6h    �� CornerRadius$ 5��6   $��Stretch=�6�   $
��auto��6�   $
��auto��6�   $��Stretch=�6�   $��3q�6�   ._�5(      �� -   5         ��Name$ BtnPrimerRegistro��5(      $��0q�68   $��59��6d    � ToolTip$' !Pulsar para ir al primer registro��6p   #��� ��6�     MaterialDesignFlatButton#��[ 6�   .��5)      �� .+�5*      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ ArrowUpBold=�66   $
��auto��6I   $��36��6V   $��Center=�6b   6�   �� 5+      $��First��6(   $��Center=�65   $��17(�6R   5,      5-      5.      �� 5/      $ 0q�6   $��1��6%   #��[ 65   $	��1.5q�6h   $ 5��6   $��Stretch=�6�   $
��auto��6�   $
��auto��6�   $��Stretch=�6�   $��3q�6�   ._�50      �� -   5        $ BtnAnteriorRegistro��50      $��0q�6:   $��59��6h   $) #Pulsar para ir al registro anterior��6t   #��[ 6�   #��� ��6�   .��51      �� .+�52        $ ArrowLeftBold=�66   $
��auto��6K   $��36��6X   $��Center=�6d   6�   �� 53      $
��Back��6(   $��Center=�64   $��17(�6Q   54      55      56      �� 58      $ 0q�6   $��2��6%   #��[ 65   $	��1.5q�6h   $ 5��6   $��Stretch=�6�   $
��auto��6�   $
��auto��6�   $��Stretch=�6�   $��3q�6�   ._�59      �� -   5        $ BtnProximoRegistro��59      $��0q�69   $��59��6f   $( "Pulsar para ir al proximo registro��6r   #��� ��6�   #��[ 6�   .��5:      �� .+�5;        $ ArrowRightBold=�66   $
��auto��6L   $��36��6Y   $��Center=�6e   6�   �� 5<      $
��Next��6(   $��Center=�64   $��17(�6Q   5=      5>      5?      �� 5A      $ 0q�6   $��3��6%   #��[ 66   $	��1.5q�6i   $ 5��6�   $��Stretch=�6�   $
��auto��6�   $
��auto��6�   $��Stretch=�6�   $��3q�6�   ._�5B      �� -   5        $ BtnUltimoRegistro��5B      $��0q�68   $��59��6d   $' !Pulsar para ir al ultimo registro��6p   #��� ��6�   #��[ 6�   .��5C      �� .+�5D        $ ArrowDownBold=�66   $
��auto��6K   $��36��6X   $��Center=�6d   6�   �� 5E      $
��Last��6(   $��Center=�64   $��17(�6Q   5F      5G      5H      �� 5J      $ 0q�6   $��4��6%   #��[ 65   $	��1.5q�6h   $ 5��6   $��Stretch=�6�   $
��auto��6�   $
��auto��6�   $��Stretch=�6�   $��3q�6�   ._�5K      �� -   5        $ 	BtnBuscar��5K      $��0q�60   $��59��6T   $$ Pulsar para buscar un registro��6`   #��� ��6�   #��[ 6�   .��5L      �� .+�5M        $ Search=�66   $
��auto��6D   $��36��6Q   $��Center=�6]   6|   �� 5N      $��Search��6(   $��Center=�66   $��17(�6S   5O      5P      5Q      �� 5S      $ 0q�6   $��5��6%   #��[ 65   $	��1.5q�6h   $ 5��6   $��Stretch=�6�   $
��auto��6�   $
��auto��6�   $��Stretch=�6�   $��3q�6�   ._�5T      �� -   5        $ 
BtnAgregar��5T      $��0q�61   $��59��6V   $% Pulsar para agregar un registro��6b   #��� ��6�   #��[ 6�   .��5U      �� .+�5V        $ AddThick=�67   $
��auto��6G   $��36��6T   $��Center=�6`   #���  6}   6�   �� 5W      $	��Add��6(   $��Center=�63   $��17(�6P   5X      5Y      5Z      �� 5[      $ 0q�6   $��6��6%   #��[ 65   $	��1.5q�6h   $ 5��6   $��Stretch=�6�   $
��auto��6�   $
��auto��6�   $��Stretch=�6�   $��3q�6�   ._�5\      �� -   5        $ 	BtnEditar��5\      $��0q�60   $��59��6T   $$ Pulsar para editar el registro��6`   #��� ��6�   #��[ 6�   .��5]      �� .+�5^        $
 Edit=�66   $
��auto��6B   $��36��6O   $��Center=�6[   6z   �� 5_      $
��Edit��6(   $��Center=�64   $��17(�6Q   5`      5a      5b      �� 5e      $ 0q�6   $��7��6%   #��[ 65   $	��1.5q�6h   $ 5��6   $��Stretch=�6�   $
��auto��6�   $
��auto��6�   $��Stretch=�6�   $��3q�6�   ._�5f      �� -	   5        $ 	BtnAnular��5f      $��0q�60   {�.  $��59��6h   $$ Pulsar para anular el registro��6t   #��� ��6�   #��[ 6�   .��5g      �� .+�5h        $ 	LockReset=�66   $
��auto��6G   $��36��6T   $��Center=�6`   6   �� 5i      $
��Undo��6(   $��Center=�64   $��17(�6Q   5j      5k      5l      �� 5o      $ 0q�6   $��8��6%   #��[ 65   $	��1.5q�6h   $ 5��6   $��Stretch=�6�   $
��auto��6�   $
��auto��6�   $��Stretch=�6�   $��3q�6�   ._�5p      �� -
   5        $ BtnCancelar��5p      $��0q�62   $��59��6X   $2 ,Pulsar para cancelar los cambios efectuados ��6d   #��� ��6�   #��[ 6�   .��5q      �� .+�5r        $ Cancel=�66   $
��auto��6D   $��36��6Q   $��Center=�6]   6|   �� 5s      $��Cancel��6(   $��Center=�66   $��17(�6S   5t      5u      5v      �� 5w      $ 0q�6   $��9��6%   #��[ 65   $	��1.5q�6h   $ 5��6   $��Stretch=�6�   $
��auto��6�   $
��auto��6�   $��Stretch=�6�   $��3q�6�   ._�5x      �� -   5        $ 
BtnGuardar��5x      $��0q�61   $��59��6V   $% Pulsar para guardar el registro��6b   #��� ��6�   #��[ 6�   $��4��6  .��5y      �� .+�5z        $ ContentSave=�67   $
��auto��6J   $��36��6W   $��Center=�6c   6�   �� 5{      $
��Save��6(   $��Center=�64   $��17(�6Q   5|      5}      5~      �� 5      $ 0q�6   $��10��6%   #��[ 66   $	��1.5q�6i   $ 5��6�   $��Stretch=�6�   $
��auto��6�   $
��auto��6�   $��Stretch=�6�   $��3q�6�   ._�5�      �� -   5        $ BtnImprimir��5�      $��0q�62   $��59��6X   $&  Pulsar para imprimir el registro��6d   #��� ��6�   #��[ 6�   .��5�      �� .+�5�        $ Printer=�66   $
��auto��6E   $��36��6R   $��Center=�6^   6}   �� 5�      $��Print��6(   $��Center=�65   $��17(�6R   5�      5�      5�      �� 5�      $ 0q�6   $��11��6&   #��[ 68   $	��1.5q�6k   $ 5��6�   $��Stretch=�6�   $
��auto��6�   $��40��6�   $��Bottom=�6�   $��3q�6�   ._�5�      �� -   5         ��Name$ lIconEstado��5�       	 �� Padding$	 0q�65   $��Center=�6A     5�      -   5        
  Name$
 
IconEstado��5�   2   $
��auto��6G   $
��auto��6T   $��Stretch=�6b   $��Stretch=�6�   
��� E��6�   5�      5�      5�      �� 5�      $��1��6   $
��Auto=�6(   .��5�      � $��
50 10 50 0q�6    5�      �� 5�      $��* �6/   �� 5�      $��* �6/   �� 5�      $��* �6/   .+�5�      � $��0��6   .+�5�      ��  �� Orientation$ Vertical=�6)   $
��auto��6@   .+�5�   "   �� $��15(�6)   $��15q�6C   #���  6O   $��2 0 0 2q�6�   $ 5��6�   ._�5�   &   �� $ Vertical=�61   .+�5�   &   �� -   5        $ lCajaID��5�   0   $��10 5 0 0q�6A   $��Caja ID��6S   $
��Bold"�6b   6v   �� 5�   *   -   5         ��Name$ 	txtCajaID��5�   2   $��30��6E   $��0q�6Q   $��1q�6]     MaterialDesignTextBoxBase#���  6�   ���	#4CEDEDED$��1��6�   ��6q   Z� 6�   5�   '   5�   #   5�      5�      � 5�      $��1��6   .+�5�      �� $ Vertical=�6)   $
��auto��6@   .+�5�   "   �� $��15(�6)   $��15q�6C   #���  6O   $��2 0 0 2q�6�   $ 5��6�   ._�5�   &   �� $ Vertical=�61   .+�5�   *   �� -   5        $ lNombre��5�   4   $��10 5 0 0q�6E   $��Nombre��6W   $
��Bold"�6e   6y   �� 5�   *   -   5        $ 	txtNombre��5�   2   $��30��6E   $��0q�6Q   $��1q�6]   #���  6�   ���	#4CEDEDED$��2��6�   ��6q   Z� 6�   5�   '   5�   #   5�      5�      � 5�      $��2��6   .+�5�      �� $ Vertical=�6)   $
��auto��6@   .+�5�      �� $��Right=�6%   $ 5q�6A   $��15(�6M   $��15 34 15 15q�6g   #���  6|   $��2q�6�   $ 5��6�   ._�5�   "   �� -   5         ��Name$ CheckActivar��5�   -   $ 
Horizontal=�6C   .+�5�   &   �� -   5         ��Name$ cActivar��5�   /   -  'MaterialDesignThemes.Wpf.CheckBoxAssist   CheckBoxSize$ 25Q�6A   $��3��6�   6�   �� 5�   &   $��Center=�60   $��Activo��6K   $
��Bold"�6Y   6m   5�   #   5�      '  !MaterialDesignThemes.Wpf.PopupBox  5�      -   5          Name$ BtnOpciones��5�   6      PlacementMode$ LeftAndAlignTopEdges=�6K   $( "Pulsar para ver opciones avanzadas��6p   $��Right=�6�   #���  6�      	StaysOpen .  $��10q�6�      PopupContent. 5   &   �� $
��auto��61   .+�5  &   �� -   5        $ BtnConfiguracionCajaLocal��5  -   $��Configurar caja local��6P   $@ :Pulsar para configurar la caja visualizada de manera local��6�   5  '   5  #   5	     5     5     5     � 5     $��2��6   .+�5     %  DevExpress.Xpf.Grid.GridControl  -   5          Name$ GridMain��5  "   )  #DevExpress.Xpf.Grid.DataControlBase   AutoGenerateColumns$ AddNew=�65   #   EnableSmartColumnsGeneration .    View 5     #  DevExpress.Xpf.Grid.TableView  5     &   DevExpress.Xpf.Grid.DataViewBase   ShowTotalSummary .    ShowAutoFilterRow . &   DevExpress.Xpf.Grid.GridViewBase   ShowGroupPanel .     	AutoWidth . 6q   5     5     5     '	  !MaterialDesignThemes.Wpf.Snackbar	  5  
   -   5         	 Name$ SnackbarThree��5  O   #��[ 6f    	  MessageQueue 6"   4
  .MaterialDesignThemes.Wpf.MessageQueueExtension
  6f   5     5     