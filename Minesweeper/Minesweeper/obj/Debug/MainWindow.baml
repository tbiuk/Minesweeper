   M S B A M L   `   `   `  ����F  BMinesweeper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MinesweeperMinesweeper  5   	       Minesweeper.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   +localclr-namespace:Minesweeper     5   	     � Title$  
MainWindow��5   	    � SizeToContent$ WidthAndHeight=�6    � 
ResizeMode$ NoResize=�5   	    � 	Resources 5         DataTemplate_Level2 �� 	Converter=   7Minesweeper.ElementStateToBackgroundColorValueConverter7   1Minesweeper.NumberToForegroundColorValueConverter J� InputBindings d� Gesture �� Command  DataTemplate_Level1 �� Orientation��%'  &	        &	 }    �� 5      .R�5	      �� $
��Bold"�5      $�� Comic Sans MS)�5      $��20��5      $��20��6!   $��1q�6,   ��5	      �� *	Content+��5
      �� *LeftClickCommand+��5      �� *ElementState+   ��5      �� *	Content+    5      d� 5      $ 
RightClicka�6#    68   �� *RightClickCommand+5      5      �� 5      .R�5   
   �� #���   6/   ��6   �� ��5      �� 5      .R�5      �� $ 
Horizontal=�6!   5      5      5      5       .��5!      � 	 � RowDefinitions	 5"      �� 5#   
   $��80 �6   6&   �� 5$   
   $��1* �6   6&   .+�5&   
   �� $
��Left=�6   $ 
Horizontal=�61   $��	10  0 0 0q�6J   .+�5'      �� $��70��6   $��70��6    $��New Game��6,   ��6?   �� *NewGameCommand+6f   �� 5)      $��Center=�6   $��10q�66   .+�5+      �� $��Broj redova��6   �� 5-      $
��Left=�6   $��30��5/      $��20��50      $��0 15 0 5q�51      ��5.      �� *NumberOfRows+51      53      �� 55      $��Center=�6   $��10q�67   .+�57      �� $��Broj stupaca��6   �� 59      $
��Left=�6   $��30��5;      $��20��5<      $��0 15 0 5q�5=      ��5:      �� *NumberOfColumns+5=      5?      �� 5A      $��Center=�6   $��10q�66   .+�5C      �� $��	Broj mina��6   �� 5E      $
��Left=�6   $��30��5G      $��20��5H      $��0 15 0 5q�5I      ��5F      �� *NumberOfMines+5I      5K      5M      �� 5O      $��1��6   #���  6J   ��6)   �� *
Elements+6J   5P      5Q      