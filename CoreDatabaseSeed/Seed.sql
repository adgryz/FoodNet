delete Products
delete Fridges
delete ProductCategories
delete FridgeProducts
delete RecipeProducts
delete Recipes
delete Tags

INSERT [dbo].[AspNetUsers] ([Id], [AccessFailedCount], [EmailConfirmed], [LockoutEnabled], [PhoneNumberConfirmed], [TwoFactorEnabled],
[Name])
 VALUES (N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad', 1, 1, 1, 1, 1, N'admin')

INSERT [dbo].[Fridges] ([Id], [UserId]) VALUES (N'edae977b-512e-4c10-9ddd-01bb14415636', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[ProductCategories] ([Id], [Name]) VALUES (N'a5ada8e6-04e1-49ca-a701-1265e216d69a', N'Spices')
INSERT [dbo].[ProductCategories] ([Id], [Name]) VALUES (N'94cae204-3337-43fa-8c0e-24c927bacac4', N'Liquids')
INSERT [dbo].[ProductCategories] ([Id], [Name]) VALUES (N'15019294-5576-4d68-8073-2ba41702f935', N'Sweets')
INSERT [dbo].[ProductCategories] ([Id], [Name]) VALUES (N'334138ce-1ee5-41b4-b22e-71398ebb3e77', N'Meat')
INSERT [dbo].[ProductCategories] ([Id], [Name]) VALUES (N'579dfa92-342f-49e2-a6c9-72e63e45afc5', N'Baking & Grains')
INSERT [dbo].[ProductCategories] ([Id], [Name]) VALUES (N'0b313634-7805-426a-86d7-82ef6286cdc9', N'Fruits')
INSERT [dbo].[ProductCategories] ([Id], [Name]) VALUES (N'6b0302bd-0791-4bab-a841-98312df661ee', N'Oils')
INSERT [dbo].[ProductCategories] ([Id], [Name]) VALUES (N'c1d14ecf-43fd-4eb1-bb1d-a009fb039239', N'Seafood')
INSERT [dbo].[ProductCategories] ([Id], [Name]) VALUES (N'08942b35-9777-46b2-b4e8-ac773c5f9ecb', N'Daiary')
INSERT [dbo].[ProductCategories] ([Id], [Name]) VALUES (N'6c09a84c-84f4-4fb3-a3cb-c57ac1ade6e8', N'Vegetables')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'55d8a632-eeb1-48ec-bfca-0050c977dedb', N'Puff Pastry', N'', N'579dfa92-342f-49e2-a6c9-72e63e45afc5', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'0faafd3d-eb76-4955-bd9f-039125489ae3', N'Trout', N'', N'c1d14ecf-43fd-4eb1-bb1d-a009fb039239', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'41271f1c-32eb-4644-9315-07dff5daf000', N'Cilantro', N'', N'a5ada8e6-04e1-49ca-a701-1265e216d69a', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'ea45f9b8-4254-4dc1-82f1-0b4227051511', N'Chicken TIghts', N'', N'334138ce-1ee5-41b4-b22e-71398ebb3e77', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'4e34c4a5-9104-480f-978c-0c4d4239d38a', N'Cauliflower', N'', N'6c09a84c-84f4-4fb3-a3cb-c57ac1ade6e8', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'f503857c-7f07-4362-88f4-0f4711962347', N'Octopus', N'', N'c1d14ecf-43fd-4eb1-bb1d-a009fb039239', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'f5e0a221-39ea-4266-a1bb-13615a101dfe', N'Pears', N'', N'0b313634-7805-426a-86d7-82ef6286cdc9', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'775ac502-2085-405b-a693-18461762fad2', N'Sausage', N'Fat but tasty', N'334138ce-1ee5-41b4-b22e-71398ebb3e77', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'896d4d7e-a3b4-4fa0-b876-1b86d3909888', N'Chicken Broth', N'', N'94cae204-3337-43fa-8c0e-24c927bacac4', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'e12d213c-f40c-4c5b-97cd-22ee43476e91', N'Pepper', N'', N'a5ada8e6-04e1-49ca-a701-1265e216d69a', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'893b4076-b53b-4725-9420-2400f30840ad', N'Potato', NULL, N'6c09a84c-84f4-4fb3-a3cb-c57ac1ade6e8', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'd5dbecbc-f080-48aa-b6cf-27b828ff3662', N'Sugar', N'', N'15019294-5576-4d68-8073-2ba41702f935', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'2e37d4df-3c03-4837-aa8f-27e19defcb14', N'Red Wine', N'', N'94cae204-3337-43fa-8c0e-24c927bacac4', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'99ad4419-3a47-4174-8975-291c7de0e6db', N'Corn', N'Yummy', N'6c09a84c-84f4-4fb3-a3cb-c57ac1ade6e8', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'a5caaf82-1ebb-4fd1-880b-2ceba830595a', N'Chilli Sauce', N'', N'94cae204-3337-43fa-8c0e-24c927bacac4', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'002d754c-176b-4324-a42e-2dc32369074d', N'Eggs', N'', N'08942b35-9777-46b2-b4e8-ac773c5f9ecb', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'549ceea7-a802-4754-8019-2ec7ac8dc238', N'Baking Powder', N'', N'579dfa92-342f-49e2-a6c9-72e63e45afc5', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'fb643e93-e672-4915-aa01-35a20ea4e6e4', N'Carrot', N'', N'6c09a84c-84f4-4fb3-a3cb-c57ac1ade6e8', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'609e68ad-1ef5-4c82-9872-3653f80de32b', N'Yeast', N'', N'579dfa92-342f-49e2-a6c9-72e63e45afc5', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'bc60ec40-6335-4e1e-ba52-38516131f9c3', N'Kiwis', N'', N'0b313634-7805-426a-86d7-82ef6286cdc9', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'4fe391a3-1bbd-4409-9128-4046095fbc66', N'Wild Rice', N'', N'579dfa92-342f-49e2-a6c9-72e63e45afc5', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'06bbed64-a505-492c-889b-472f7dab0faa', N'Butter', N'', N'6b0302bd-0791-4bab-a841-98312df661ee', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'270b1180-45c6-4ee1-bc06-4819743859dc', N'Cream', NULL, N'08942b35-9777-46b2-b4e8-ac773c5f9ecb', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'154fa98b-5938-4c6b-ab3b-4849e3613283', N'Oranges', N'', N'0b313634-7805-426a-86d7-82ef6286cdc9', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'73023991-a72c-45fe-88ab-4a05f4fbb65e', N'Cheese', NULL, N'08942b35-9777-46b2-b4e8-ac773c5f9ecb', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'1c87bb73-cd90-473f-9756-4d16bf09264c', N'Plums', N'', N'0b313634-7805-426a-86d7-82ef6286cdc9', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'51c5ed6d-a749-4e24-acea-53aba7b638e5', N'Carp', N'', N'c1d14ecf-43fd-4eb1-bb1d-a009fb039239', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'54df18f9-c2d9-44d3-b854-58c53a53caeb', N'Red Onion', N'', N'6c09a84c-84f4-4fb3-a3cb-c57ac1ade6e8', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'638a413e-4b88-4369-b485-5b7b209d5bfd', N'Eel', N'', N'c1d14ecf-43fd-4eb1-bb1d-a009fb039239', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'b979e491-3c6e-443e-b142-5cd73a3fc7fa', N'Spaghetti', N'', N'579dfa92-342f-49e2-a6c9-72e63e45afc5', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'cf28a9a5-2cf8-417d-8852-5dd0627ec2cd', N'Onion', N'Key to goood dinner', N'6c09a84c-84f4-4fb3-a3cb-c57ac1ade6e8', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'f0015f52-6c93-452e-a340-608c860fe5ab', N'Peas', N'', N'6c09a84c-84f4-4fb3-a3cb-c57ac1ade6e8', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'df85d038-b526-4cb2-aa45-666f39569a93', N'Oysters', N'', N'c1d14ecf-43fd-4eb1-bb1d-a009fb039239', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'8946d8a4-4413-4e19-89c0-6c5846800ba7', N'Curry', N'', N'a5ada8e6-04e1-49ca-a701-1265e216d69a', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'738b71e4-c4de-4c9c-b315-6c6edf51627f', N'Cottage Cheese', N'', N'08942b35-9777-46b2-b4e8-ac773c5f9ecb', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'd1fe3d23-cee6-446d-87aa-6c94ccacea19', N'Chocolade', N'', N'15019294-5576-4d68-8073-2ba41702f935', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'6f707e82-35a5-4cda-89a7-70fe09a5eae3', N'Ricotta', N'', N'08942b35-9777-46b2-b4e8-ac773c5f9ecb', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'b0e2c109-9e14-42b4-b247-755ef13c29a7', N'Apples', N'', N'0b313634-7805-426a-86d7-82ef6286cdc9', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'361742cc-9efa-4450-aac7-77ffcf6cfac8', N'Paprika', N'', N'6c09a84c-84f4-4fb3-a3cb-c57ac1ade6e8', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'6028c0dc-6090-48c2-8f74-7b98222566c6', N'Yogurt', N'', N'08942b35-9777-46b2-b4e8-ac773c5f9ecb', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'598ab867-7eab-416c-be9d-7cf29ffea5a7', N'Broccoli', N'', N'6c09a84c-84f4-4fb3-a3cb-c57ac1ade6e8', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'9c750539-3ca6-4239-941f-805b81c38cd4', N'Salmon', N'', N'c1d14ecf-43fd-4eb1-bb1d-a009fb039239', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'eef42c7d-22d2-4616-a95b-826d5d247fbf', N'Ribs', N'Good for BBQ', N'334138ce-1ee5-41b4-b22e-71398ebb3e77', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'0ae4a36d-d955-4a2f-a368-88a6013b95d8', N'Limes', N'', N'0b313634-7805-426a-86d7-82ef6286cdc9', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'04e0915d-a67d-429b-a0d9-88e40de1bbbf', N'Strawberries', N'', N'0b313634-7805-426a-86d7-82ef6286cdc9', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'f6bec1af-cccf-4bcc-886b-8b499f812b4c', N'Parmigiano-Reggiano', N'', N'08942b35-9777-46b2-b4e8-ac773c5f9ecb', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'9c0143e0-65de-4b34-b3f5-8e1fa3c16455', N'Jalapenios', N'', N'6c09a84c-84f4-4fb3-a3cb-c57ac1ade6e8', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'30b6a00a-b831-4d5c-8f0e-8f3fb70a2ed5', N'Beef', NULL, N'334138ce-1ee5-41b4-b22e-71398ebb3e77', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'3dd77560-4854-405f-97df-8fe799f58ff1', N'Cinnamon', N'', N'a5ada8e6-04e1-49ca-a701-1265e216d69a', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'4345a3b3-6f0c-475c-97fa-90507fd114b2', N'Cardamon', N'', N'a5ada8e6-04e1-49ca-a701-1265e216d69a', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'54ff5549-a50c-456a-acb5-920423e98b54', N'Basil', N'', N'a5ada8e6-04e1-49ca-a701-1265e216d69a', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'ee8130aa-edcf-4968-984f-9224c5ba3244', N'Beer', N'', N'94cae204-3337-43fa-8c0e-24c927bacac4', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'904d8959-a3f0-45bb-b110-92464a05aad9', N'Chicken Breasts', N'', N'334138ce-1ee5-41b4-b22e-71398ebb3e77', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'0ea3ea42-c9a6-42e5-9e83-9459596ea3e4', N'Maple Syrup', N'', N'15019294-5576-4d68-8073-2ba41702f935', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'04a092b3-edc8-4e8c-8da2-96d37b18a6df', N'Garlic', N'', N'6c09a84c-84f4-4fb3-a3cb-c57ac1ade6e8', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'faa49e3d-1598-4f31-a448-9823f4d564d2', N'Pineapple', N'', N'0b313634-7805-426a-86d7-82ef6286cdc9', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'7366faa0-3573-4d6a-b249-9b817a3ef84b', N'Tomato', N'Super tasty !', N'6c09a84c-84f4-4fb3-a3cb-c57ac1ade6e8', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'72d389f9-13b7-463a-9d56-a29bfb96fd74', N'White Chocolade', N'', N'15019294-5576-4d68-8073-2ba41702f935', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'b06712dd-5937-40e5-942f-ab10f8bb1003', N'Bananas', N'', N'0b313634-7805-426a-86d7-82ef6286cdc9', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'193c4a1a-d6f1-465d-a31a-aed4f9f8a6ab', N'Mint', N'', N'a5ada8e6-04e1-49ca-a701-1265e216d69a', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'1a12d7d3-5ac7-47ad-a624-b072d547828d', N'Sour Cream', N'', N'08942b35-9777-46b2-b4e8-ac773c5f9ecb', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'6df1691f-b235-4e84-b3a3-b0cd76e3f3f4', N'Fries', N'', N'579dfa92-342f-49e2-a6c9-72e63e45afc5', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'39598f3d-f2ad-4a9c-950c-b22ac8f060dd', N'Milk', N'Good for bones', N'08942b35-9777-46b2-b4e8-ac773c5f9ecb', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'c9bb475d-6119-4df9-b266-b40903be30e1', N'Pasta', N'', N'579dfa92-342f-49e2-a6c9-72e63e45afc5', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'c19a455b-7d73-43fb-96f0-b454600eb376', N'Cherries', N'', N'0b313634-7805-426a-86d7-82ef6286cdc9', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'cf17d26b-b205-4fd0-b23c-b505aa8de0a9', N'Oil', N'', N'6b0302bd-0791-4bab-a841-98312df661ee', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'21a4cd7e-f100-4606-a7fb-b5b094a8a5c3', N'White Wine', N'', N'94cae204-3337-43fa-8c0e-24c927bacac4', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'9d8fac6f-c194-44e5-a6d5-b6f6dbdebbd0', N'Mustard', N'', N'a5ada8e6-04e1-49ca-a701-1265e216d69a', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'6af309ec-b6db-45b5-9416-bc63f31ad6ba', N'Chicken', N'Finger lickin'' good', N'334138ce-1ee5-41b4-b22e-71398ebb3e77', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'3c2de8ae-4c6f-430d-a1a6-bec13068b08f', N'Bacon', NULL, N'334138ce-1ee5-41b4-b22e-71398ebb3e77', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'bc6d7e84-6e0c-4d5e-a9bd-c1a2386c24e7', N'Bread', N'', N'579dfa92-342f-49e2-a6c9-72e63e45afc5', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'4ea6667a-57fd-4e98-be50-c7d15003ac50', N'Chicken WIngs', N'', N'334138ce-1ee5-41b4-b22e-71398ebb3e77', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'4f8298e7-dc11-41f4-8bc0-c8d0e4c0618b', N'Salt', N'', N'a5ada8e6-04e1-49ca-a701-1265e216d69a', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'13c800f4-4926-4431-8020-c9ad7f6fcb61', N'Cherries', N'', N'a5ada8e6-04e1-49ca-a701-1265e216d69a', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'2252c0d7-11d0-4f06-be1f-c9c485c5a175', N'Rice', N'', N'579dfa92-342f-49e2-a6c9-72e63e45afc5', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'a4144688-a808-47a5-bd17-ca06270aa222', N'Flour', N'', N'579dfa92-342f-49e2-a6c9-72e63e45afc5', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'c7eb1d4b-5f76-4531-a45f-cbc11401ad04', N'Buckwheat Groats', N'', N'579dfa92-342f-49e2-a6c9-72e63e45afc5', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'9271900b-0891-410a-92e6-cea039202785', N'Millet Groats', N'', N'579dfa92-342f-49e2-a6c9-72e63e45afc5', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'f7218369-9197-42a2-a25e-cef7f2eeff17', N'Avocados', N'', N'0b313634-7805-426a-86d7-82ef6286cdc9', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'0e9664bf-fe28-44df-b2ba-d011bf4d681a', N'Shrimps', N'', N'c1d14ecf-43fd-4eb1-bb1d-a009fb039239', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'8c8845ab-1f06-4ddc-8d49-d387c7779f28', N'Basil', N'', N'a5ada8e6-04e1-49ca-a701-1265e216d69a', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'f1f5610d-f065-46d8-9208-d7d1a0cb8c27', N'Brown Sugar', N'', N'15019294-5576-4d68-8073-2ba41702f935', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'da660749-983e-4220-949d-d89ea6faf74f', N'Flounder', N'', N'c1d14ecf-43fd-4eb1-bb1d-a009fb039239', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'3d7781c6-f678-47ff-9d97-db7bf5b119a6', N'Tagliatelle', N'', N'579dfa92-342f-49e2-a6c9-72e63e45afc5', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'91032ba1-9546-4444-add7-e2bf0e290046', N'Lemons', N'', N'0b313634-7805-426a-86d7-82ef6286cdc9', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'c51b7318-03c1-499d-b695-e3d74a4a0303', N'Cocoa', N'', N'15019294-5576-4d68-8073-2ba41702f935', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'8089f2e7-b40a-47d9-803a-ea332907feca', N'Aubergine', N'', N'6c09a84c-84f4-4fb3-a3cb-c57ac1ade6e8', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'78ce35c6-6d26-45f5-bf82-f1ef8be2898f', N'Olive Oil', N'', N'6b0302bd-0791-4bab-a841-98312df661ee', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'bc5c38ed-dd1f-48d8-8a5f-f662d2adfcd5', N'Beef Broth', N'', N'94cae204-3337-43fa-8c0e-24c927bacac4', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'c62cc051-eed7-44c0-ac40-f6e4a8560c15', N'Thyme', N'', N'a5ada8e6-04e1-49ca-a701-1265e216d69a', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Products] ([Id], [Name], [Description], [ProductCategoryId], [UserId]) VALUES (N'5a03dbc7-d1c4-4c37-aa75-fe8f40d3ad2a', N'Pork Loin', N'', N'334138ce-1ee5-41b4-b22e-71398ebb3e77', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
UPDATE [dbo].[Products] set [Discriminator] = N'BasicProduct'
INSERT [dbo].[FridgeProducts] ([Id], [FridgeId], [ProductId]) VALUES (N'022e5c46-2413-42de-aca5-03f63cb1e16d', N'edae977b-512e-4c10-9ddd-01bb14415636', N'270b1180-45c6-4ee1-bc06-4819743859dc')
INSERT [dbo].[FridgeProducts] ([Id], [FridgeId], [ProductId]) VALUES (N'6797a748-d128-4d00-88e4-671a0101a3e8', N'edae977b-512e-4c10-9ddd-01bb14415636', N'73023991-a72c-45fe-88ab-4a05f4fbb65e')
INSERT [dbo].[FridgeProducts] ([Id], [FridgeId], [ProductId]) VALUES (N'90812383-c64d-4aae-bca2-9882de527ecd', N'edae977b-512e-4c10-9ddd-01bb14415636', N'06bbed64-a505-492c-889b-472f7dab0faa')
INSERT [dbo].[FridgeProducts] ([Id], [FridgeId], [ProductId]) VALUES (N'37683dff-f496-4c6e-91ff-a002f9cd3704', N'edae977b-512e-4c10-9ddd-01bb14415636', N'002d754c-176b-4324-a42e-2dc32369074d')
INSERT [dbo].[FridgeProducts] ([Id], [FridgeId], [ProductId]) VALUES (N'6dd8e367-ba28-4ee6-9b9b-db00d7f4fdef', N'edae977b-512e-4c10-9ddd-01bb14415636', N'39598f3d-f2ad-4a9c-950c-b22ac8f060dd')
INSERT [dbo].[Recipes] ([Id], [Title], [Description], [UserId]) VALUES (N'83bd2a25-83ea-47d0-9b7b-0e4d528cf8c2', N'Salmon with Brown Sugar Glaze', N'1. Preheat the oven''s broiler and set the oven rack at about 6 inches from the heat source; prepare the rack of a broiler pan with cooking spray.

2. Season the salmon with salt and pepper and arrange onto the prepared broiler pan. Whisk together the brown sugar and Dijon mustard in a small bowl; spoon mixture evenly onto top of salmon fillets.

3. Cook under the preheated broiler until the fish flakes easily with a fork, 10 to 15 minutes.', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Recipes] ([Id], [Title], [Description], [UserId]) VALUES (N'e68a68d6-bf32-41dc-a58a-1f554db892b3', N'Simple sausage rolls', N'Preheat oven to180C.
Separate 4 sheets of puff pastry to defrost and cut each sheet in half.
Mix sausage mince with diced onion and carrot.
Evenly divide the mix into 8 portions and lie along each pastry rectangle.
Roll one edge of the sausage mix and tuck the mix evenly under it with a knife.
Brush egg wash on the other edge and roll the pastry on top. Slice this into 3 and lie on a baking paper lined oven tray.
Continue until all the sausage mix is rolled and placed on the trays.
Brush each roll with the egg wash and cook for approximately 25-20 mins.', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Recipes] ([Id], [Title], [Description], [UserId]) VALUES (N'67216200-43d2-401f-b3c8-2da88fdb3a10', N'Grilled Cheese Sandwich', N'1. Heat a nonstick skillet over medium heat with the butter. When the butter is melted. Lightly moisten both pieces of bread in the butter. Top one slice of bread with the provolone and tomato and cover with the other slice of bread. Cook until golden brown on both sides, flipping a couple of times.

2. When the basic grilled cheese is done, remove it from the pan. Turn the heat to medium. If the pan seems very dry, melt a touch more butter in it. Sprinkle the Parmigiano in the still-hot skillet, in a shape roughly the same as the sandwich.

3. Now watch closely, as the cheese melts. You''ll see the fat start to cook out and the cheese begin to brown and crisp. When it has just begun to brown, put the sandwich back on top and press down with a large spatula to weld the frico to the bread. Let cook for one more minute then serve.', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Recipes] ([Id], [Title], [Description], [UserId]) VALUES (N'd3f7dd4b-1113-4fd0-893b-39fd0183b0a8', N'Guacamole', N'1 Cut avocado, remove flesh: Cut the avocados in half. Remove seed. Score the inside of the avocado with a blunt knife and scoop out the flesh with a spoon. (See How to Cut and Peel an Avocado.) Place in a bowl.

2 Mash with a fork: Using a fork, roughly mash the avocado. (Don''t overdo it! The guacamole should be a little chunky.)

3 Add salt, lime juice, and the rest: Sprinkle with salt and lime (or lemon) juice. The acid in the lime juice will provide some balance to the richness of the avocado and will help delay the avocados from turning brown.
Add the chopped onion, cilantro, black pepper, and chiles. Chili peppers vary individually in their hotness. So, start with a half of one chili pepper and add to the guacamole to your desired degree of hotness.
Remember that much of this is done to taste because of the variability in the fresh ingredients. Start with this recipe and adjust to your taste.

4 Cover with plastic and chill to store: Place plastic wrap on the surface of the guacamole cover it and to prevent air reaching it. (The oxygen in the air causes oxidation which will turn the guacamole brown.) Refrigerate until ready to serve.
Chilling tomatoes hurts their flavor, so if you want to add chopped tomato to your guacamole, add it just before serving.', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Recipes] ([Id], [Title], [Description], [UserId]) VALUES (N'264f504a-b248-465f-b5b0-4825c02c9b8e', N'Basic pancakes', N'1. Gather all ingredients.

2. In a bowl, whisk flour and sugar. Beat in the egg and then milk a little at a time until batter is smooth and lump-free.

3. In a hot pan or flat grill over medium heat, brush butter over cooking surface and pour 1/4 cup measures for each pancake.
 
4. When large bubbles form on the surface, flip the pancake over and cook until lightly golden on the other side.', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Recipes] ([Id], [Title], [Description], [UserId]) VALUES (N'cbe61c20-38fe-4626-8cd8-557b1d8307f3', N'Perfect Mashed Potatoes', N'1 Cover peeled, cut potatoes with cold, salted water, simmer until tender: Place the peeled and cut potatoes into a medium saucepan. Add cold water to the pan until the potatoes are covered by at least an inch. Add a half teaspoon of salt to the water.
Turn the heat on to high, and bring the water to a boil. Reduce the heat to low to maintain a simmer, and cover. Cook for 15 to 20 minutes, or until you can easily poke through them with a fork.
2 Melt butter, warm cream: While the potatoes are cooking, melt the butter and warm the cream. You can heat them together in a pan on the stove or in the microwave.
3 Drain cooked potatoes, mash with butter, cream, milk: When the potatoes are done, drain the water and place the steaming hot potatoes into a large bowl. Pour the heated cream and melted butter over the potatoes.
Mash the potatoes with a potato masher. Then use a strong wooden spoon (a metal spoon might bend) to beat further.
Add milk and beat until the mashed potatoes are smooth. Don''t over-beat the potatoes or the mashed potatoes will end up gluey.
Add salt and pepper to taste.
', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Recipes] ([Id], [Title], [Description], [UserId]) VALUES (N'f795b317-db3b-469f-9891-62c5ccc9df5d', N'Soft Scrambled Eggs', N'In a medium bowl, whisk together the eggs, mascarpone, salt and pepper. Be thorough, but don''t worry if there are little bits of mascarpone flecking the egg. Heat a large, non-stick skillet over a medium-low flame. Add the butter and wait until it melts before adding the egg. If the egg starts to cook right away, turn the heat as low as it will go. Using a wooden spatula or spoon, stir the eggs constantly, scraping the bottom of the pan all over in a long, continuous motion. Do this for about 10 minutes, adjusting the heat as necessary (raise it a little if the egg touching the pan isn''t cooking at all; lower it if it starts to cook quickly or look at all dry). Pull the eggs off the heat when they''re still a little custardy-looking, but not runny -- they''ll cook a bit more as you pile them onto your plate. Divide the eggs among two warm plates and serve immediately, with toast if you like, and more ground black pepper.', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Recipes] ([Id], [Title], [Description], [UserId]) VALUES (N'84e494a4-296d-46ca-b73d-e3d8a7df3dc7', N'Frying pan pizza with aubergine', N'1.Weigh the ingredients for the dough into a large bowl and add 1/2 tsp salt and 125ml warm water. Mix to form a soft dough, then tip onto your work surface and knead for 5 mins or until the dough feels stretchy. Clean and grease the bowl and return the dough. Cover with cling film and leave somewhere warm to rise for 1 hr, or until the dough has doubled in size.

2.Meanwhile, make the sauce. Heat 1 tbsp olive oil in a pan and add the garlic. Sizzle gently for 30 secs, making sure the garlic doesn’t brown, then add the passata. Season well and bubble for 8-10 mins until you have a rich sauce – add a pinch of sugar if it tastes a little too tart. Set aside.

3. When the dough has risen, knock out the air and roll it into a pizza base the same size as a large frying pan. Oil the surface of the dough, cover with cling film , then leave on the work surface for 15 mins to puff up a little. Meanwhile, heat 2 tbsp oil in the frying pan and add the aubergines in a single layer (you may have to cook in batches). Season well and cook for 4-5 mins on each side until really tender and golden. Transfer to a dish and cover with foil to keep warm.

4. Heat the remaining 1 tbsp of oil in the pan and carefully lift the dough into it. You may have to reshape it a little to fit. Cook over a low-medium heat until the underside is golden brown and the edges of the dough are starting to look dry and set – this should take about 6 mins, but it’s best to go by eye. Flip over, drizzle a little more oil around the edge of the pan so it trickles underneath the pizza base, and cook for another 5-6 mins until golden and cooked through. Reheat the sauce if you need to and spread it over the base. Top with the warm aubergines and dot with spoonfuls of ricotta. Scatter with mint and drizzle with a little extra virgin olive oil just before serving.', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Recipes] ([Id], [Title], [Description], [UserId]) VALUES (N'662b2b0a-695f-43e9-92cc-e40e3fbe9bbf', N'Hawaiian Pineapple Sweet & Sour Smoked Sausage', N'1. Cook and stir sausage and bell peppers over medium-high heat for 5 minutes or until sausage is lightly browned and peppers are tender.
2. Add chili garlic sauce and pineapple; cook and stir for 5 minutes.
3. Serve over cooked rice.', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[Recipes] ([Id], [Title], [Description], [UserId]) VALUES (N'6f4a376e-a721-44d6-8772-ebcd539b5511', N'Damn Good French Toast', N'Slice challah into 3/4 to 1-inch thick slices.
Whisk together eggs and cream. Heat a griddle or flat grill pan over medium high heat, and add 1 tbsp butter for every two pieces of French toast it will accommodate; swirl butter around to cover surface. 
Dip slice of bread in egg and cream; flip and repeat. 
Add to griddle, and grill until golden (approximately 90 seconds) on one side; flip and repeat. 
Serve with additional butter, if desired, and good quality maple syrup. 
The experience is enhanced with the addition of good quality smoked bacon.', N'b71dbc28-6a02-47b1-8a2a-c6a5bfcd78ad')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'cc56a882-a035-46d7-9c5f-01b41fe5eb9f', N'e68a68d6-bf32-41dc-a58a-1f554db892b3', N'002d754c-176b-4324-a42e-2dc32369074d')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'16d36c57-f735-4c9c-8623-069fe30c0324', N'264f504a-b248-465f-b5b0-4825c02c9b8e', N'a4144688-a808-47a5-bd17-ca06270aa222')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'1cccedd2-31e0-40fb-b6e4-06df6b07b5c4', N'84e494a4-296d-46ca-b73d-e3d8a7df3dc7', N'04a092b3-edc8-4e8c-8da2-96d37b18a6df')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'ff112204-acd3-46f7-8218-10597e2962c6', N'83bd2a25-83ea-47d0-9b7b-0e4d528cf8c2', N'9c750539-3ca6-4239-941f-805b81c38cd4')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'c5c3c1ee-65d0-487d-91f3-140bb86f60be', N'84e494a4-296d-46ca-b73d-e3d8a7df3dc7', N'cf17d26b-b205-4fd0-b23c-b505aa8de0a9')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'c193f3e3-6e7c-42ab-999d-19b2d7fadbb0', N'662b2b0a-695f-43e9-92cc-e40e3fbe9bbf', N'faa49e3d-1598-4f31-a448-9823f4d564d2')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'611a4912-8e51-49b6-a6ba-20672606b830', N'cbe61c20-38fe-4626-8cd8-557b1d8307f3', N'893b4076-b53b-4725-9420-2400f30840ad')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'097f8efb-2e06-45a6-9ba0-26e51b4bf580', N'84e494a4-296d-46ca-b73d-e3d8a7df3dc7', N'a4144688-a808-47a5-bd17-ca06270aa222')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'ff7edff6-145e-46d6-a9b7-2e4e039f7826', N'cbe61c20-38fe-4626-8cd8-557b1d8307f3', N'4f8298e7-dc11-41f4-8bc0-c8d0e4c0618b')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'5a51380e-8ec8-48e5-82ce-2fde6bc2132f', N'662b2b0a-695f-43e9-92cc-e40e3fbe9bbf', N'e12d213c-f40c-4c5b-97cd-22ee43476e91')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'9dca4241-d112-45a4-a389-39892d7d4779', N'84e494a4-296d-46ca-b73d-e3d8a7df3dc7', N'6f707e82-35a5-4cda-89a7-70fe09a5eae3')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'8738b038-a0cf-45f2-a2d4-3a098f09bfee', N'e68a68d6-bf32-41dc-a58a-1f554db892b3', N'775ac502-2085-405b-a693-18461762fad2')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'30b1bdb3-f8f3-49ee-8e74-41ee903a5854', N'84e494a4-296d-46ca-b73d-e3d8a7df3dc7', N'609e68ad-1ef5-4c82-9872-3653f80de32b')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'6836e0b4-0b6d-4b97-9234-43f089cbcea4', N'83bd2a25-83ea-47d0-9b7b-0e4d528cf8c2', N'9d8fac6f-c194-44e5-a6d5-b6f6dbdebbd0')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'5ba907e9-7bf0-4722-a66a-461338b3ac3f', N'84e494a4-296d-46ca-b73d-e3d8a7df3dc7', N'd5dbecbc-f080-48aa-b6cf-27b828ff3662')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'4faa11a8-9291-4f56-babb-4a5084c0d963', N'67216200-43d2-401f-b3c8-2da88fdb3a10', N'bc6d7e84-6e0c-4d5e-a9bd-c1a2386c24e7')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'91afefe3-717a-4748-8a56-4de6083bcc48', N'6f4a376e-a721-44d6-8772-ebcd539b5511', N'002d754c-176b-4324-a42e-2dc32369074d')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'b1c655c5-42e2-4cf2-8d90-4e09c3d4268d', N'264f504a-b248-465f-b5b0-4825c02c9b8e', N'd5dbecbc-f080-48aa-b6cf-27b828ff3662')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'0a538c1c-855a-4158-8783-4fbbc68db003', N'662b2b0a-695f-43e9-92cc-e40e3fbe9bbf', N'2252c0d7-11d0-4f06-be1f-c9c485c5a175')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'3c1b0310-971f-4000-8fd9-566363fd6c50', N'd3f7dd4b-1113-4fd0-893b-39fd0183b0a8', N'41271f1c-32eb-4644-9315-07dff5daf000')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'8ab2b584-ab2c-4f0e-86d8-573ae2a52a80', N'84e494a4-296d-46ca-b73d-e3d8a7df3dc7', N'193c4a1a-d6f1-465d-a31a-aed4f9f8a6ab')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'7fde6c3f-99b2-4886-8cb0-582023efdbb2', N'd3f7dd4b-1113-4fd0-893b-39fd0183b0a8', N'4f8298e7-dc11-41f4-8bc0-c8d0e4c0618b')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'3501b527-7e98-4fde-9fbc-5a6d47c9289d', N'84e494a4-296d-46ca-b73d-e3d8a7df3dc7', N'8089f2e7-b40a-47d9-803a-ea332907feca')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'c4b12487-56ff-487e-8efc-5c6dc2731d35', N'd3f7dd4b-1113-4fd0-893b-39fd0183b0a8', N'0ae4a36d-d955-4a2f-a368-88a6013b95d8')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'62ab39e6-f0e5-44e7-a3ee-66cf8bd79226', N'6f4a376e-a721-44d6-8772-ebcd539b5511', N'270b1180-45c6-4ee1-bc06-4819743859dc')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'2354a7c4-4134-4c63-abb3-6b3a0f260595', N'84e494a4-296d-46ca-b73d-e3d8a7df3dc7', N'7366faa0-3573-4d6a-b249-9b817a3ef84b')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'14469158-7d5e-4342-aea0-6b8471dbd969', N'67216200-43d2-401f-b3c8-2da88fdb3a10', N'7366faa0-3573-4d6a-b249-9b817a3ef84b')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'9ed96bba-599d-444c-930a-6b993011e740', N'67216200-43d2-401f-b3c8-2da88fdb3a10', N'73023991-a72c-45fe-88ab-4a05f4fbb65e')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'28e746ca-992f-4227-91ee-6de07bd2f6aa', N'67216200-43d2-401f-b3c8-2da88fdb3a10', N'f6bec1af-cccf-4bcc-886b-8b499f812b4c')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'a81ed463-ad05-4f80-819f-72317eb0d1ad', N'f795b317-db3b-469f-9891-62c5ccc9df5d', N'06bbed64-a505-492c-889b-472f7dab0faa')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'c967c77c-35b3-4d37-ba9e-7491f81e98f4', N'cbe61c20-38fe-4626-8cd8-557b1d8307f3', N'39598f3d-f2ad-4a9c-950c-b22ac8f060dd')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'7335c264-eb61-42a3-a268-7e722b4734b7', N'264f504a-b248-465f-b5b0-4825c02c9b8e', N'39598f3d-f2ad-4a9c-950c-b22ac8f060dd')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'2ee9e45e-4494-4aec-b31b-87e33ec7c255', N'f795b317-db3b-469f-9891-62c5ccc9df5d', N'270b1180-45c6-4ee1-bc06-4819743859dc')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'814dbebf-d459-4826-a809-8a97be834203', N'f795b317-db3b-469f-9891-62c5ccc9df5d', N'002d754c-176b-4324-a42e-2dc32369074d')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'c44975fb-d836-46cc-b694-8e4bc40d4229', N'd3f7dd4b-1113-4fd0-893b-39fd0183b0a8', N'f7218369-9197-42a2-a25e-cef7f2eeff17')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'72927ecf-3d99-49ae-807b-8f9a1092d707', N'd3f7dd4b-1113-4fd0-893b-39fd0183b0a8', N'e12d213c-f40c-4c5b-97cd-22ee43476e91')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'82f9b1f1-9249-48fc-95db-90fc981d20fe', N'cbe61c20-38fe-4626-8cd8-557b1d8307f3', N'270b1180-45c6-4ee1-bc06-4819743859dc')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'03cb0317-b7dd-4634-ac71-9b2d71d434af', N'67216200-43d2-401f-b3c8-2da88fdb3a10', N'06bbed64-a505-492c-889b-472f7dab0faa')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'6875e352-ff03-4b19-9129-9e7a6820f956', N'e68a68d6-bf32-41dc-a58a-1f554db892b3', N'39598f3d-f2ad-4a9c-950c-b22ac8f060dd')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'a3f7ea77-1850-469b-8e4c-a4a207ff554c', N'cbe61c20-38fe-4626-8cd8-557b1d8307f3', N'06bbed64-a505-492c-889b-472f7dab0faa')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'db505245-22d0-485d-a4c8-a9c1b031d94b', N'6f4a376e-a721-44d6-8772-ebcd539b5511', N'bc6d7e84-6e0c-4d5e-a9bd-c1a2386c24e7')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'02b158d9-be99-4a03-b9d0-ac06918efac2', N'264f504a-b248-465f-b5b0-4825c02c9b8e', N'06bbed64-a505-492c-889b-472f7dab0faa')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'3e3c7e3e-79e9-4f86-adbf-b25b5a9afbca', N'6f4a376e-a721-44d6-8772-ebcd539b5511', N'06bbed64-a505-492c-889b-472f7dab0faa')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'e078550f-c078-4f2f-8188-b6a15e262aef', N'e68a68d6-bf32-41dc-a58a-1f554db892b3', N'cf28a9a5-2cf8-417d-8852-5dd0627ec2cd')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'844aaacd-5643-4e91-9220-b6c41fb4c5c0', N'264f504a-b248-465f-b5b0-4825c02c9b8e', N'002d754c-176b-4324-a42e-2dc32369074d')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'97d8f11f-1d07-46d6-919b-cd0288e09727', N'e68a68d6-bf32-41dc-a58a-1f554db892b3', N'55d8a632-eeb1-48ec-bfca-0050c977dedb')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'7fcbfe00-5f84-4329-a146-d2f9d3ae7f21', N'6f4a376e-a721-44d6-8772-ebcd539b5511', N'0ea3ea42-c9a6-42e5-9e83-9459596ea3e4')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'067bec3a-42c2-4367-bbab-d61c5b3d5aad', N'662b2b0a-695f-43e9-92cc-e40e3fbe9bbf', N'a5caaf82-1ebb-4fd1-880b-2ceba830595a')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'2c4b224f-634a-4a15-9e42-e683f6b2ddd7', N'e68a68d6-bf32-41dc-a58a-1f554db892b3', N'fb643e93-e672-4915-aa01-35a20ea4e6e4')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'b21e069c-4af2-4c26-9c5e-ed562e7c7b42', N'662b2b0a-695f-43e9-92cc-e40e3fbe9bbf', N'361742cc-9efa-4450-aac7-77ffcf6cfac8')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'91c27bcb-9d87-447a-bb42-ee6ccdf0c802', N'662b2b0a-695f-43e9-92cc-e40e3fbe9bbf', N'775ac502-2085-405b-a693-18461762fad2')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'bbe6c2ad-569c-4dfb-8215-efbdcb75c385', N'd3f7dd4b-1113-4fd0-893b-39fd0183b0a8', N'7366faa0-3573-4d6a-b249-9b817a3ef84b')
INSERT [dbo].[RecipeProducts] ([Id], [RecipeId], [ProductId]) VALUES (N'2e7765c4-db63-4db7-a7c7-f9d9a80bb03b', N'83bd2a25-83ea-47d0-9b7b-0e4d528cf8c2', N'f1f5610d-f065-46d8-9208-d7d1a0cb8c27')



INSERT [dbo].[Tags] ([Id], [Text]) VALUES (N'f240813c-a078-49f9-9556-7f057149f8ad', N'vege')
INSERT [dbo].[Tags] ([Id], [Text]) VALUES (N'e942e9cf-caa9-4c94-beb8-967d37ff85e4', N'tasty')
INSERT [dbo].[Tags] ([Id], [Text]) VALUES (N'556f96c3-1ef1-4cb0-aecc-cf2b46b98524', N'fancy')
INSERT [dbo].[Tags] ([Id], [Text]) VALUES (N'64e9ed30-8a52-4d7a-949f-dfcf36dcb7ec', N'food')
INSERT [dbo].[Tags] ([Id], [Text]) VALUES (N'00cc50f9-a0b2-4bab-b6f8-96e791389953', N'meat')
INSERT [dbo].[Tags] ([Id], [Text]) VALUES (N'c44f0ec6-393f-42d1-a287-c37e2804fc48', N'fruits')

insert [dbo].[RecipeTags] VALUES (N'1f21e7d0-1c13-40ab-b1ea-004a533cd8f3', N'F795B317-DB3B-469F-9891-62C5CCC9DF5D', N'F240813C-A078-49F9-9556-7F057149F8AD')
insert [dbo].[RecipeTags] VALUES (N'8e12a9b4-70bd-47ea-8d5f-e6f6c9c9d882', N'F795B317-DB3B-469F-9891-62C5CCC9DF5D', N'E942E9CF-CAA9-4C94-BEB8-967D37FF85E4')
insert [dbo].[RecipeTags] VALUES (N'dcd078f2-ecb4-46a3-94eb-2b643340857f', N'83BD2A25-83EA-47D0-9B7B-0E4D528CF8C2', N'E942E9CF-CAA9-4C94-BEB8-967D37FF85E4')
insert [dbo].[RecipeTags] VALUES (N'1773e382-7881-42df-97fe-957a849591d5', N'6F4A376E-A721-44D6-8772-EBCD539B5511', N'F240813C-A078-49F9-9556-7F057149F8AD')