   M S B A M L   `   `   `  ����G  Cimpiccato v1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:impiccato_v1impiccato_v1  5   	       impiccato_v1.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:impiccato_v1     5   	   8
syncfusion!http://schemas.syncfusion.com/wpf     5   	   :xctk)http://schemas.xceed.com/wpf/xaml/toolkit     5   	     � Title$  Impiccato - Impostazioni��5
   	   $��349.501��6*   $��499.846��6;    � 
ResizeMode$ NoResize=�6K   
���   ����	#FF38B9B4.��5      � ��5   
   �� 5       � ColumnDefinitions 5   
   �� 5      $
��189* �6   �� 5      $	��14* �6   �� 5      $
��149* �6   .+�5   
   �� $#��Seleziona modalità di gioco:��6   $
��Left=�67   $	��Top=�6R   $��	10,10,0,0q�6j   $	��280��6}   $��3��6�   �� 5   
   -   5         ��Name$ 
cbModalita��5      $
��Left=�6'   $��	10,41,0,0q�6B   $	��Top=�6U   $	��186��6m   $��2��6�   .@�5      �� $��Singleplayer��6   �� 5      $��Multiplayer (CPU)��6   �� 5      $��Multiplayer (Locale)��6   5      �� 5   
   -   5         ��Name$	 pn1��5      $
��Left=�6"   $	��219��6=   $��10,80,-43,0q�6J   $	��Top=�6_   $	��385��6w   $y�Visible=�6�   $��3��6�   .+�5      �� $��Seleziona la difficoltà:��6   $
��Left=�67   $	��Top=�6R   $	��280��6j   �� 5      -   5        $ cbDifficolta��5      $
��Left=�6-   $	��Top=�6H   $	��186��6`   .@�5      �� $��Facile��6   �� 5      $��Medio��6   �� 5      $��	Difficile��6   5       �� 5!      -   5        $ pnLingua��5!      $
��Left=�6+   $	��169��6F   $	��Top=�6S   $	��375��6k   $y�Visible=�6w   .+�5"      �� $��Seleziona la lingua:��6   $
��Left=�67   $	��Top=�6R   $	��280��6j   �� 5#      -   5        $ cbLingua��5#      $
��Left=�6-   $	��Top=�6H   $	��186��6`   .@�5$      �� $��Italiano��6#   �� 5%      $��Inglese��6#   �� 5&      $��Spagnolo��6#   5'      5(      5)      �� 5*   
   -   5         ��Name$ btnGioca��5*      $��Gioca��6#   $
��Left=�63   $	��Top=�6N   $	��469��6f   $��80��6r   {�.  $��
10,219,0,0q�6�   $��3��6�   
���   ���5+      �� 5,       �� Color$ White��6"   5.      �� 5/   
   -   5        $ pnNGiocatori��5/      $
��Left=�6+   $	��175��6F   $��
246,10,0,0q�6S   $	��Top=�6g   $	��238��6   $��3��6�   .+�50      �� $��Numero Giocatori:��6   $
��Left=�60   $	��Top=�6K   $	��110��6c   �� 51      -   5         ��Name$ txtNGiocatori��51      $��23��6-    �� TextWrapping$
 Wrap=�69   $��0��6M   52      53      54      