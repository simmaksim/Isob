   M S B A M L   `   `   `  ���� F  BMDiSUBD_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MDiSUBD_WPFMDiSUBD_WPF      MDiSUBD_WPF.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:MDiSUBD_WPF       � Title$  Service Station��$��408.8��$��961.6�� � Icon$< 6C:\PTUIR\5 semestr\MDiSUBD\MDiSUBD_WPF\Images\Logo.jpg��.��� $��0,182,-0.4,8.2q� � RowDefinitions �� $��* ��� $
��Auto �.+�&   System.Windows.Controls.DataGrid  -     Name$ infoGrid��   AutoGenerateColumns .     SelectionMode$ Single=�$
��Left=�$	��289��$��
8,-168,0,0q�$	��Top=�$	��935��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 30 	   
IsReadOnly	 . 
   SortMemberPath$
  ��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *id+  $ Заказчик��$ 70 	 .  �� *c_name+  $ СТО��$ 70 	 .  �� *w_name+  $ Номер заказа��$ 90  �� *number+  $ Запчасть��$ 80 	 .  �� *d_name+  $ Цена з-ти��$ 80 	 .  �� *	d_price+  $ Фото��$ 90 	 .  �� *p_name+  $ Мастер��$ 80 	 .  �� *m_name+  $ Работа��$	 100 	 .  �� *	wt_name+  $ Стоим. р-ты��$ 80 	 .  �� *
wt_price+  $ Оплата��$ 80 	 .  �� *	pt_name+  $ 
Почта��$ 80 	 .  �� *u_name+�� -   $��Добавить��$
��Left=�$��
10,152,0,0q�$	��Top=�$��75���� -   $��Delete��$��Удалить��$
��Left=�$��
90,152,0,0q�$	��Top=�$��75���� -   $��Сохранить��$
��Left=�$��172,152,0,0q�$	��Top=�$��75���� -   $��
Поиск��$
��Left=�$��867,152,0,0q�$	��Top=�$��75���� -   $��
SearchText��$
��Left=�$��23��$��740,150,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$	��120���� -   $��По заказчику��$
��Left=�$��375,152,0,0q�$	��Top=�$��80���� -   $��По цене��$
��Left=�$��460,152,0,0q�$	��Top=�$��75�� J� RenderTransformOrigin$ 0.647,0.611&��� -	   $��SearchCombo��$
��Left=�$��620,152,0,0q�$	��Top=�$	��112�� �� SelectedIndex$ 0��.@��� Заказчик�� Тип работы�� Мастер�� -
   $��	logButton��$��Журнал��$
��Left=�$��540,152,0,0q�$	��Top=�$��75���� -   $��addCustomerButton��$)��#Добавить заказчика��$
��Left=�$��253,152,0,0q�$	��Top=�$	��120��