   M S B A M L   `   `   `  ���� G  CMessenger, Version=1.1.300.49, Culture=neutral, PublicKeyToken=null%clr-namespace:Messenger	Messenger  ,    &Messenger.Notification.UsersNotifyIcon   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Cie:http://schemas.microsoft.com/expression/2010/interactivity  "mainclr-namespace:Messenger  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  $	��250��$	��300��  � DataContext  ��    Messenger.MainViewModel   Instance �� Source# Z .���� $��5q�   AccentColorBrush#���    J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 10Q�   	Direction$ 0Q�   ShadowDepth$ 0Q�  AccentColor   Color# �  ._��  � RowDefinitions �� $��30 ��� .+��� $	��5,0q�$��Center=�	 �� Orientation$	 
Horizontal=�.+��� $��20��$��20��$��Center=�$4��./Messenger;component/Icons/Conference-Call.png��$��Uniform=��� $	��Top=�$	��Top=�
�������$��16(�$
��Bold"�
������� $  SelfMessageMaterialDesignTextBox#��[ $��	MESSENGER��� $��1��$��1q�  
WhiteBrush#���  .+��� -   
 ��Name$
 ListBoxMemebers��$��5q�$��Stretch=�$��Stretch=�$
��Auto=�$
��Auto=�  MaterialDesignCardsListBox#��[ ����  �� Path$ Members� �� UpdateSourceTrigger$ PropertyChanged=�.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ PreviewMouseDoubleClick��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� # Z $$ OpenUserMessengerWindowCommand�   CommandParameter ��  �� ElementName$ ListBoxMemebers��$ .����� .R�� $��13(�  	TextBrush#���   � ColumnDefinitions �� $��50 ��� .+��� $��0��$��30��$��30��$��Right=� K� ZIndex$ 3�� * &MaterialDesignFloatingActionDarkButton#��[ ���� # Z $$ OpenUserMessengerWindowCommand�����  � ToolTip ��   Locator# [ $0 *LocalizationHelper.[Resources.WriteToUser]�.��(� $��18��$��18��.��F� $��Center=�$��Center=��������k����&�Q��o]��$?�@@�����k���k1^����kqM�p�KqM�p�K0֧���k0֧p    1.�"�T@��AX�?1.�"�T@Q{p� ��,��h0@ѽB���Q�l��,��h0@Q�,1.�"�T@Q�-1.�"�T@��- &�yI# &�yI#�[T�    �[T�\��Z	@    �s�    �ge�    �GJ"Գ �s��gѡ�� "Գ 	#���   �� Stretch$ Uniform=��� $��1��$��8 0 0 0q�$
��Left=�.+��� $��14(�$
��Bold"� �� 
IsReadOnly . ���� *
UserName+�� $
��Left=�$��Center=�$	 
Horizontal=�.+��� $
��Bold"����� # [ $, &LocalizationHelper.[Resources.Machine]� �� StringFormat$ {0} :���� ���� $ MachineName��� $
��Left=�$��Center=�$	 
Horizontal=�.+��� $
��Bold"����� # [ $. (LocalizationHelper.[Resources.IPAddress]�$ {0} :���� ���� $ 	IPAddress����� .R��� 