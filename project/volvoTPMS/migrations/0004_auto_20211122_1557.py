# Generated by Django 3.2.8 on 2021-11-22 14:57

from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    dependencies = [
        ('volvoTPMS', '0003_auto_20211122_1455'),
    ]

    operations = [
        migrations.RemoveField(
            model_name='company',
            name='vehicles',
        ),
        migrations.AddField(
            model_name='vehicle',
            name='company',
            field=models.ForeignKey(default=None, on_delete=django.db.models.deletion.CASCADE, to='volvoTPMS.company'),
        ),
    ]