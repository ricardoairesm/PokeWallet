   M S B A M L   `   `   `  ����E  APokeWallet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PokeWallet
PokeWallet  5   	       PokeWallet.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:PokeWallet     5   	     � Title$  
MainWindow��5   	   $	��755��6   $
��1408��6)   .��5	      � $��Center=�6   $
��1465��6(   ��5
   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ Black��6   � 5      $ 	#FF6CCE1D��6    � Offset$ 1Q�61   5       � ColumnDefinitions 5   
   �� 5      $
��762* �6   �� 5      $
��703* �6   .+�5   
   �� $��202,82,157,514q�6>   $��1��6�    �� SelectedItem 6V   �� *TreinadorSelecionado+��6�   �� *TrainerList+ �� View 5      �� 5      .P�5      �� $
��Name��6%    �� Width$	 100��6Z   	 �� DisplayMemberBinding	 63   �� *Name+6Z   �� 5      $��Id��6%   $ 60��6V   	 61   �� *Id+6V   �� 5      $	��Age��6%   $ 60��6X   	 62   �� *Age+6X   �� 5      $
��Team��6%   $	 100��6f   	 63   �� *PokeWalletString+6f   5      5      �� 5   
   $��	Add to Pc��60   $
��Left=�6D   $��
84,521,0,0q�6_   $	��Top=�6s   $��35��6�   $	��106��6�   ��6   �� *
AddPokemon+6�   �� 5   
   $��Remove from Pc��63   $
��Left=�6L   $��259,521,0,0q�6g   $	��Top=�6|   
 J� RenderTransformOrigin$
 0.508,0.645&�6�   $��36��6�   $	��132��6�   ��6   �� *RemovePokemon+6�   �� 5    
   $��Give Nickname��63   $
��Left=�6K   $��440,523,0,0q�6f   $	��Top=�6{   $
 0.555,0.657&�6�   $��32��6�   $	��132��6�   ��6   �� *UpdatePokemon+6�   �� 5!   
   $��Add to team��62   $
��Left=�6H   $��
52,154,0,0q�6c   $	��Top=�6w   $��31��6�   $��78��6�   $��1��6�   ��6   �� *CatchPokemon+6�   �� 5"   
   $��
0,82,0,259q�6>   $��Center=�6�   $	��630��6�    6R   �� *PokemonSelecionado+��6~   �� *
PokeList+ 5#      �� 5$      .P�5%      �� $
��Name��6%   $	 100��63   	 6?   �� *Name+�� 5&      $��Nickname��6%   $	 100��67   	 6C   �� *
Nickname+�� 5'      $��Types��6%   $	 150��64   	 6@   �� *Type+�� 5(      $��PokeId��6%   $ 60��65   	 6@   �� *PokeId+�� 5)      $��Id��6%   $ 60��61   	 6<   �� *Id+�� 5*      $	 100��6%   $��Sprite��61    �� CellTemplate 5+      �� 5,      .R�5-   "   � .+�5.   &   �� ��6,   �� *Sprite+5/   #   50      52      53      55      �� 56   
   $��Add Trainer��62   $
��Left=�6H   $��202,261,0,0q�6c   $	��Top=�6x   $��30��6�   $��92��6�   $��1��6�   ��6   �� *AddTreinador+6�   �� 57   
   $��Show Wallet��67   $
��Left=�6M   $��332,337,0,0q�6h   $	��Top=�6}   $��30��6�   $��92��6�   $��1��6�   ��6   �� *ShowTrainerWallet+6�   �� 58   
   $��Remove Trainer��66   $
��Left=�6O   $��454,261,0,0q�6j   $	��Top=�6   $��30��6�   $��92��6�   $��1��6�   ��6   �� *RemoveTreinador+6�   �� 59   
   $����https://upload.wikimedia.org/wikipedia/commons/thumb/9/98/International_Pok%C3%A9mon_logo.svg/800px-International_Pok%C3%A9mon_logo.svg.png��6   $
��Left=�6�   $��62��6�   $��	40,10,0,0q�6�   $	��Top=�6�   $	��188��6�   �� 5:   
   $��Update Trainer��65   $
��Left=�6N   $��332,261,0,0q�6i   $	��Top=�6~   $��30��6�   $��92��6�   $��1��6�   ��6   �� *UpdateTreinador+6�   5=      5>      