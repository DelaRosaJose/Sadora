   M S B A M L   `   `   `  ����A  =Sadora, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Sadora.AdministracionSadora.Administracion  5   	   '    !Sadora.Ventas.FrmControlFormaPago   5   	   a ]DevExpress.Xpf.Grid.v19.1, Version=19.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af bDevExpress.Xpf.Grid.v19.1.Core, Version=19.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v19.1, Version=19.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:Sadora.Administracion     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   -   5        $X��Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)�5   	    "  SecundaryAccentForegroundBrush#���   5
   	     � Title$  FrmControlComprobantes��5   	    � WindowState$ 	Maximized=�6(    � WindowStyle$
 None=�6@    � AllowsTransparency . 
������ .��5      � .+�5   
   )  #MaterialDesignThemes.Wpf.DialogHost  -   5          Name$ MiniDialogo��5   3      IsOpen .     DialogContent 5      � 5      $	��700��6   $
��auto��6#   $��0q�61   ���	#19C8C8C8$��Center=�6S    � RowDefinitions 5      �� 5      $��15 �6(   �� 5      $
��auto �6(   �� 5      $��* �6(   �� 5      $
��auto �6(   .+�5      �� $��1��6!    �� Orientation$ 
Horizontal=�6.   $��Right=�6G   $��	0 10 20 0q�6d   .+�5      �� $��Right=�6$   $��Monto:  ��6@   $��29(�6P   $
��Bold"�6^   �� 5      -   5        	 ��Name$	 lMonto��5   $   $��Right=�64   $��
110,000.00��6P   $��29(�6b   ���	#FF75AC57$
��Bold"�6�   5      �� 5      $
��auto��6!   $��2��6/   $��20q�6@   $��1��6L   $	��Top=�6Y   $
��Bold"�6q   .@�5       �� -   5        
 ��Name$
 TabItem��5    <   $��Desglose de pago��6"   $��25(�6M   .��5Q      �  5R   "   �� 5S   &   $��* �64   �� 5T   &   $��* �64   .+�5W   "   �� $��20(�6)   $��0��6C   $��15q�6P     PrimaryHueDarkBrush#���  6\   $��2 0 0 2q�6�    �� CornerRadius$ 5��6�   ._�5X   &   �� $ Vertical=�61   .+�5Y   *   �� -   5        $	 lMontoDistribuido��5Y   4   $��10 5 0 0q�6O   $
��Bold"�6�   ��6a   �� *FormaPagoAplicada+ �� UpdateSourceTrigger$ PropertyChanged=� �� StringFormat$ Monto a pagar con {0}��6�   �� 5Z   *   -   5         ��Name$ txtMontoDistribuido��5Z   2   $��30��6O   $��0q�6[   $��1q�6g     MaterialDesignTextBoxBase#���  6�   ���	#4CEDEDED$��1��6�   ��6{   Z� 6  5[   '   5\   #   �� 5^   "   $��20(�6)   $��1��6D   $��15q�6Q   #���  6]   $��2 0 0 2q�6�   $ 5��6�   ._�5_   &   �� $ Vertical=�61   .+�5`   *   �� -   5        $	 lMontoRestante��5`   4   $��10 5 0 0q�6L   $��Monto faltante��6^   $
��Bold"�6t   �� 5a   *   -   5        $ txtMontoRestante��5a   2    �� 
IsReadOnly . $��30��6^   $��0q�6j   $��1q�6v   #���  6�   ���	#4CEDEDED��6�   Z� 6�   5b   '   5c   #   5e      5f      5g      � 5k      $��3��6   .+�5l      #  MaterialDesignThemes.Wpf.Card  -	   5          Name$ PanelOpcionesPagos��5l   _   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth2=�6.   $y�Hidden=�6{   $��0��6�   $��0 4 0 4q�6�   .��5n      � -
   5         �Name$ 	PanelWrap��5n   D   $��Right=�6(   5�      5�      � 5�      -   5         �Name$ BtAceptarCancelar��5�      $y�Visible=�6:    � ColumnDefinitions 5�      �� 5�   "   $��* �63   �� 5�   "   $��* �63   .+�5�      ��  �� Padding$ 0q�6%   $��0��61   #��[ 6A   $	��1.5q�6t   $ 5��6�   $��Stretch=�6�   $
��auto��6�   $
��auto��6�   $��Stretch=�6�   $��
20 10 8 10q�6�   ._�5�   "   �� -   5         ��Name$ 
btnAceptar��5�   )   $��35(�6=   $
��auto��6K   $��2��6r   #��� ��6     MaterialDesignFlatButton#���  6�   $��0q�6�   .��5�   &   �� $ 
Horizontal=�61   .+�5�   *   '  !MaterialDesignThemes.Wpf.PackIcon  $��Center=�6B      Kind$ 	CheckBold=�6]   
��� � �$��35��6�   $��35��6�   6�   �� 5�   *   $��5 0 0 0q�64   $��Center=�6E   	ACEPTAR6j   5�   '   5�   #   5�      �� 5�      $ 0q�6%   $��1��61   #��[ 6A   $	��1.5q�6t   $ 5��6�   $��Stretch=�6�   $
��auto��6�   $
��auto��6�   $��Stretch=�6�   $��
8 10 20 10q�6�   ._�5�   "   �� -   5        $ btnCancelar��5�   )   $��35(�6?   $
��auto��6M   #��� ��6u   #���  6�   .��5�   &   �� $ 
Horizontal=�61   .+�5�   *     $��Center=�6B   $ Cancel=�6]   
���  ��$��35��6|   $��35��6�   6�   �� 5�   *   $��5 0 0 0q�64   $��Center=�6E   
CANCELAR6k   5�   '   5�   #   5�      5�      5�      '  !MaterialDesignThemes.Wpf.Snackbar  5�      -   5          Name$ SnackbarThree��5�   h   $��2��6.   #��[ 6      MessageQueue 6;   4  .MaterialDesignThemes.Wpf.MessageQueueExtension  6   5     5`     5b     5c     