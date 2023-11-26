# life-quality-back

## Table of Contents
- [Introduction](#authors)
- [Installation](#introduction)
- [Usage](#important)

## <a name="authors"></a> Authors:

## <a name="prerequisites"></a> Prerequisites
Необхідно мати встановлений MS SQL Server. 
У мене це була версія Developer 2022

## <a name="introduction"></a> Introduction
Після стягнення проекту необхідно відкрити 
1. через меню Tools -> 
2. NuGet Package Manager ->
3. Package Manager Console

Після цього в консолі Package Manager (умовне позначення в консолі буде PM>) необхідно ввести команду `Update-Database`.

## <a name="important"></a> Important
1. Якщо ви вносили зміни в моделі і хочете, щоб вони відобразилися в базі даних, необхідно відкрити Package Manager Console (див. [Introduction](#authors))
2. Запустити команду `Add-Migration <назва міграції одним словом латиницею>`, щоб Entity Framework згенерував міграцію
3. Запустити команду `Update-Database`, щоб внести зміни в структуру БД