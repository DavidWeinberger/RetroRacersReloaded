   M S B A M L   `   `   `  ����N  JRetroRacersReloaded, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:RetroRacersReloadedRetroRacersReloaded  5   	   *    $RetroRacersReloaded.playerInitialize   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:RetroRacersReloaded     5   	     � Title$  playerInitialize��5   	   $
��1080��6"   $
��1920��60    � WindowStyle$
 None=�6=    � WindowState$ 	Maximized=�6P   .��5	      � ��5
   
   �� 5       �� ImageSource$ /Resources/background.jpg��6   6C   .+�5   
   �� -   5        $��+��5      $��Center=�6   $��	550,0,0,0q�6:   $��Center=�6M   $	��140��6h   $	��100��6t   $��70(�6�   6�   �� 5   
   -   5        $��-��5      $��Center=�6   $��	0,0,550,0q�6:   $��Center=�6M   $	��140��6h   $	��100��6t   $��70(�6�   6�   �� 5   
   $��Center=�6   $��Center=�6-   $	��110��6H   $��70��6U   .��5      �� -   5        $��PlayerAmount��5      $	��100(�6(   $��	ExtraBold"�67   $��Chiller)�6N   5      �� 5   
   $��Center=�6   $��	0,124,0,0q�6-   $	��Top=�6@   $	��110��6X   $	��600��6e   .��5      �� $��Spieler hinzufügen��6   $	��100(�61   $��Heavy"�6@   $��Chiller)�6S   5      �� 5   
   $
��Left=�6   $��
50,0,0,200q�6+   $��Bottom=�6?   $	��110��6Z   $	��600��6g   .��5      �� -   5        $��p1��5      $&�� Steuerung Spieler Blau W|S|A|D: ��6   $��50(�6I   $��Heavy"�6W   $��Chiller)�6j   
��� ���6   5      �� 5   
   $
��Left=�6   $��
50,0,0,100q�6+   $��Bottom=�6?   $	��110��6Z   $	��600��6g   .��5      �� -   5        $��p3��5      $&�� Steuerung Spieler Grün: I|K|J|L��6   $��50(�6H   $��Heavy"�6V   $��Chiller)�6i   
��� ���6~   5      �� 5   
   $��Right=�6   $��
0,0,50,100q�6,   $��Bottom=�6@   $	��110��6[   $	��600��6h   .��5      �� -   5        $��p4��5      $%��Steuerung Spieler Gelb: 8|5|4|6��6"   $��50(�6L   $��Heavy"�6Z   $��Chiller)�6m   
��� ���6�   5      �� 5   
   $��Right=�6   $��
0,0,50,200q�6,   $��Bottom=�6@   $	��110��6[   $	��600��6h   .��5       �� -   5        $��p2��5       $,��&Steuerung Spieler Rot: ↑|↓|←|→��6&   $��50(�6O   $��Heavy"�6]   $��Chiller)�6p   
��� ���6�   5!      �� 5"   
   -   5        $��Zurück��5"      $
��Left=�6"   $��	50,50,0,0q�6=   $	��Top=�6P   $	��109��6h   $��37��6t   6�   �� 5#   
   -	   5        $��Start��5#      $��Right=�6!   $��	0,50,50,0q�6=   $	��Top=�6P   $	��109��6i   $��37��6u   6�   5%      5&      