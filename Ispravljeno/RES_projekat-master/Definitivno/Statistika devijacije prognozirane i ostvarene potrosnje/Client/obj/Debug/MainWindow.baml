   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ClientClient  5   	       Client.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Client     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)   .��5	      � 
�����_�.+�5
   
   c� $��
0,45,0,238q�6   .@�5      �� $��Izvestaj��6   �� 5      -   5        $��Izvestaj��5      $��30��6'   $��50��63   $��0,0,0,1q�6]   ��6>   �� *datumi+6]   �� 5      $��Region��6   �� 5      -   5        $��Regioni��5      $��30��6&   $��50��62   ��6=   �� *zem+�� 5      $��Od��6   �� 5      -   5        $��SatiOd��5      $��30��6%   $��50��61   ��6<   �� *sat+�� 5      $��Do��6   �� 5      -   5        $��SatiDo��5      $��30��6%   $��50��61   ��6<   �� *sat+�� 5      -   5         ��Name$ Prikazi��5      $��Prikazi��6&   $��31��68   $	��Top=�6D   $��75��6\   6g   5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��Tabela��5      $
��Left=�6!   $	��194��6<   $��	10,94,0,0q�6I   $	��Top=�6\   $	��497��6t      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$	 Sat��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 62   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� *sat+  5      $ Prognozirana potrosnja��6%   $ * 6E    6O   �� *prog+  5      $ Ostvarena potrosnja��6%   $ * 6B    6L   �� *izm+  5      $ 
Devijacija��6%   $ * 69    6C   �� *dev+5      �� 5   
   -   5         ��Name$ label��5      $��Prosecna devijacija:��6   $
��Left=�6>   $��231,288,0,0q�6Y   $	��Top=�6n   $��31��6�   �� 5    
   -   5        $ labelProsek��5       $�� ��6%   $
��Left=�60   $��348,288,0,0q�6K   $	��Top=�6`   $	��159��6x   �� 5!   
   -	   5        $ 	labelUvoz��5!      $��	XML File:��6#   $
��Left=�67   $��	10,10,0,0q�6R   $	��Top=�6e   $��23��6}   �� 5"   
   -
   5         ��Name$ textBoxUvoz��5"      $
��Left=�6'   $��23��6B   $��	67,10,0,0q�6N    �� TextWrapping$
 Wrap=�6a   $�� ��6u   $	��Top=�6}   $	��120��6�   �� 5#   
   -   5        $ buttonNadji��5#      $��Nadji��6&   $
��Left=�66   $��
192,10,0,0q�6Q   
�������$	��Top=�6y   $��75��6�   $��23��6�   6�   �� 5$   
   -   5        $ buttonUvezi��5$      $��Uvezi��6&   $
��Left=�66   $��
268,10,0,0q�6Q   
�������$	��Top=�6{   $��75��6�   $��23��6�   6�   �� 5%   
   -   5        $ labelUpozorenje��5%      $�� ��6)   $
��Left=�64   $��
348,10,0,0q�6O   $	��Top=�6c   $��30��6{   $	��140��6�   5'      5(      