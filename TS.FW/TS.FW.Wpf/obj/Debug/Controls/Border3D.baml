   M S B A M L   `   `   `  ����K  GTS.FW.Wpf, Version=1.0.9127.30919, Culture=neutral, PublicKeyToken=null7 clr-namespace:TS.FW.Wpf.ControlsTS.FW.Wpf.Controls  5      !    TS.FW.Wpf.Controls.Border3D   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:TS.FW.Wpf.Controls     5      -   5          >�Name$  xRoot��5      |�.  ��5	      �� 5
   
    �� 
TargetType" >�6    � 	Resources 5      -  'System.Windows.Media.Effects.BlurEffect   xLayoutEffect   Radius   
KernelType��%9   &	          5      $ 2Q�63   $ Gaussian=�6>   5      .R�5      �� #��z��6   
������ $��0q�6Y    �� CornerRadius 5      �� *CornerRadius+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType!   TS.FW.Wpf.Controls.Border3D"  ._�5      � .+�5      �� 	 ��Name$	 xBlack��6   $��0 0 1 1q�6-   
���   �
 J� Effect#
 [  6�    6[   �� *CornerRadius+ �� $ FindAncestor=�"  6�   �� 5      $	 xWhite��6   $��1 1 0 0q�6-   
�������#
 [  6�   $��1q�6   6[   �� *CornerRadius+ �� $ FindAncestor=�"  6  �� 5      #��z��6'   #��z��5   '   #��z��5   '   #��z��5   '   #��z��5   '   #��z��5   '   #��z��5   '   #��z��5   '   ��5   '   �� *ContentMargin+ �� $ FindAncestor=�"  5   '   5      5       �� Triggers 5       P� 5!       P� Property �@ 	IsPressed P� Value . . �5"      ��  �� 
TargetName$ xBlack��6    �� Property � �� �� Value
 �����6H   �� 5#      $ xWhite��6    � ��
 �   �6H   �� 5$      $ xBlack��6    � ��$ 2q�6C   �� 5%      $ xWhite��6    � ��$ 0q�6C   �� 5&      $ xBlack��6    �@J�Opacity$	 0.5Q�6D   5'      5)      5+      