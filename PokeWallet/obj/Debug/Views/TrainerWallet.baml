   M S B A M L   `   `   `  ����E  APokeWallet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PokeWallet
PokeWallet  5   	       PokeWallet.TrainerWallet   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:PokeWallet     5   	     � Title$  TrainerWallet��5   	   $	��617��6   $	��844��6,   .��5	      � $��Center=�6   $	��844��6(   ��5
   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ Black��6   � 5      $ 	#FF1CC81F��6    � Offset$ 1Q�61   5      .+�5   
   �� $��0,0,0,6q�6>   $
��Left=�6�   $	��516��6�   ��6O   �� *
PokeList+ �� SelectedItem 6p   �� *PokemonSelecionado+ �� View 5      �� 5      .P�5      �� $
��Name��6%    �� Width$	 100��63    �� DisplayMemberBinding 6?   �� *Name+�� 5      $��Nickname��6%   $	 100��67    6C   �� *
Nickname+�� 5      $��Types��6%   $	 150��64    6@   �� *Type+�� 5      $��PokeId��6%   $ 50��65    6@   �� *Id+�� 5      $	 100��6%   $��Sprite��61   	 �� CellTemplate	 5      �� 5      .R�5   "   � .+�5   &   �� ��6,   �� *Sprite+5   #   5      5      5       5"      �� 5#   
   $��Remover do Time��6   $
��Left=�6M   $��590,210,0,0q�6h   $	��Top=�6}   $��46��6�   $	��111��6�   ��6+   �� *RemovePokemon+6�   �� 5$   
   $��Treinador :��6   $
��Left=�6&   $��
551,44,0,0q�6A   $	��Top=�6U   $��24��6m   $��68��6y   ���	#FFF9F6F6
 J� RenderTransformOrigin$
 0.287,0.564&�6�   �� 5%   
   $
��Left=�6/   $��
646,44,0,0q�6J   $��24��6^   $	��Top=�6j   $
 	1.3,2.204&�6�   ���	#FFF9F6F6��6   �� *
Owner.Name+6�   5'      5(      