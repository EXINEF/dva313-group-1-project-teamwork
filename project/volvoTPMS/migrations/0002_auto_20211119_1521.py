# Generated by Django 3.2.8 on 2021-11-19 14:21

from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    dependencies = [
        ('volvoTPMS', '0001_initial'),
    ]

    operations = [
        migrations.AddField(
            model_name='sensor',
            name='status',
            field=models.CharField(max_length=30, null=True),
        ),
        migrations.AddField(
            model_name='tire',
            name='fill_material',
            field=models.CharField(blank=True, max_length=30, null=True),
        ),
        migrations.AddField(
            model_name='tire',
            name='revolutions',
            field=models.IntegerField(blank=True, null=True),
        ),
        migrations.AddField(
            model_name='tire',
            name='tread_depth',
            field=models.IntegerField(blank=True, null=True),
        ),
        migrations.AddField(
            model_name='vehicle',
            name='ambient_temperature',
            field=models.IntegerField(blank=True, null=True),
        ),
        migrations.AddField(
            model_name='vehicle',
            name='consumed_fuel',
            field=models.FloatField(blank=True, null=True),
        ),
        migrations.AddField(
            model_name='vehicle',
            name='model',
            field=models.CharField(blank=True, max_length=50, null=True),
        ),
        migrations.AddField(
            model_name='vehicle',
            name='payload',
            field=models.FloatField(blank=True, null=True),
        ),
        migrations.AddField(
            model_name='vehicle',
            name='weight',
            field=models.FloatField(blank=True, null=True),
        ),
        migrations.AlterField(
            model_name='sensor',
            name='pressure',
            field=models.IntegerField(blank=True, null=True),
        ),
        migrations.AlterField(
            model_name='sensor',
            name='remaning_battery',
            field=models.IntegerField(blank=True, null=True),
        ),
        migrations.AlterField(
            model_name='sensor',
            name='temperature',
            field=models.IntegerField(blank=True, null=True),
        ),
        migrations.AlterField(
            model_name='tire',
            name='baseline_pressure',
            field=models.IntegerField(blank=True, null=True),
        ),
        migrations.AlterField(
            model_name='tire',
            name='remaining_life',
            field=models.IntegerField(blank=True, null=True),
        ),
        migrations.AlterField(
            model_name='tire',
            name='sensor',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to='volvoTPMS.sensor'),
        ),
        migrations.AlterField(
            model_name='vehicle',
            name='distance_driven',
            field=models.IntegerField(blank=True, null=True),
        ),
        migrations.AlterField(
            model_name='vehicle',
            name='machine_hours',
            field=models.IntegerField(blank=True, null=True),
        ),
        migrations.AlterField(
            model_name='vehicle',
            name='tires',
            field=models.ManyToManyField(blank=True, null=True, to='volvoTPMS.Tire'),
        ),
    ]
