from django.apps import AppConfig


class TpmsConfig(AppConfig):
    default_auto_field = 'django.db.models.BigAutoField'
    name = 'tpms'

    def ready(self):
    	import tpms.signals
