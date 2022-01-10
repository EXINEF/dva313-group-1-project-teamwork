from django.shortcuts import get_object_or_404, redirect, render
from django.contrib.auth.forms import AuthenticationForm
from django.contrib.auth import authenticate, login, logout
from .models import Company, FleetManager, Vehicle, Tire, Sensor
from .forms import VehicleForm, TireForm, SensorForm, VehicleFormOnlyTires
from .decorators import unauthenticated_user, fleet_manager_only
from django.contrib.auth.decorators import login_required
from django.contrib import messages

@unauthenticated_user
def indexPage(request):
    if request.method == 'POST':
        username = request.POST.get('username')
        password = request.POST.get('password')

        user = authenticate(request, username=username, password=password)

        if user is not None:
            login(request, user)
            # redirect to index, that will redirect in home or admin
            return redirect('index')
        else:
            messages.error(request, 'Username OR password is incorrect')

    context = {}
    return render(request,'index/index.html', context)

@login_required(login_url='index')
def logoutPage(request):
    logout(request)
    return redirect('index')

def countVehiclesInStatus(vehicles, status):
    counter = 0
    for vehicle in vehicles:
        if vehicle.getStatus()[0] == status:
            counter += 1
    return counter

def countSensorsInventory(sensors):
    counter = 0
    for sensor in sensors:
        if sensor.getStatus()[0] != 'OK':
            counter += 1
    return counter

@login_required(login_url='index')
@fleet_manager_only
def homePage(request):
    fleet_manager = FleetManager.objects.get(user=request.user)
    vehicles = Vehicle.objects.filter(company=fleet_manager.company)
    tires = Tire.objects.filter(company=fleet_manager.company)
    sensors = Sensor.objects.filter(company=fleet_manager.company)
    vehicles_warning_num = countVehiclesInStatus(vehicles, 'WARNING')
    vehicles_danger_num = countVehiclesInStatus(vehicles, 'DANGER')
    inventory_not_ok_num = countSensorsInventory(tires) + countSensorsInventory(sensors)

    tires_num = tires.count()
    sensors_num = sensors.count()

    context = {'vehicles':vehicles, 'fleet_manager':fleet_manager, 'tires':tires, 'tires_num':tires_num, 'sensors_num':sensors_num, 'vehicles_warning_num':vehicles_warning_num, 'vehicles_danger_num':vehicles_danger_num, 'sensors':sensors, 'inventory_not_ok_num':inventory_not_ok_num, }
    return render(request, 'user/home.html', context) 

@login_required(login_url='index')
@fleet_manager_only
def allTires(request):
    fleet_manager = FleetManager.objects.get(user=request.user)
    tires = Tire.objects.filter(company=fleet_manager.company)

    context = {'tires':tires, 'fleet_manager':fleet_manager,}
    return render(request, 'user/inventory/all-tires.html', context) 

@login_required(login_url='index')
@fleet_manager_only
def allSensors(request):
    fleet_manager = FleetManager.objects.get(user=request.user)
    sensors = Sensor.objects.filter(company=fleet_manager.company)

    context = {'sensors':sensors, 'fleet_manager':fleet_manager,}
    return render(request, 'user/inventory/all-sensors.html', context) 

@login_required(login_url='index')
@fleet_manager_only
def vehicle(request, pk):
    fleet_manager = FleetManager.objects.get(user=request.user)
    vehicle = get_object_or_404(Vehicle, id=pk, company=fleet_manager.company)
    locations = vehicle.locations.all().order_by('creation_datetime')
    lastLoaction = locations.first()

    context = {'vehicle':vehicle, 'locations':locations , 'lastLocation':lastLoaction}
    return render(request, 'user/vehicle/vehicle.html', context)

@login_required(login_url='index')
@fleet_manager_only
def addVehicle(request):
    fleet_manager = FleetManager.objects.get(user=request.user)
    form = VehicleForm(company=fleet_manager.company)
    
    if request.method == 'POST':
        form = VehicleForm(request.POST, company=fleet_manager.company)
        if form.is_valid():
            new_vehicle = form.save(commit=False)
            new_vehicle.company = fleet_manager.company
            new_vehicle.save()
            form.save_m2m()
            messages.success(request,'The new vehicle was addedd successfuly')
            return redirect('home')

    context = {'form':form}
    return render(request, 'user/vehicle/add-vehicle.html', context)

@login_required(login_url='index')
@fleet_manager_only
def editVehicle(request, pk):
    fleet_manager = FleetManager.objects.get(user=request.user)
    vehicle = get_object_or_404(Vehicle, id=pk, company=fleet_manager.company)
    form = VehicleForm(instance=vehicle, company=fleet_manager.company)
    
    if request.method == 'POST':
        form = VehicleForm(request.POST, instance=vehicle, company=fleet_manager.company)

        if form.is_valid():
            form.save()
            messages.success(request,'The vehicle %s is been update successfuly' % vehicle)
            return redirect('vehicle', vehicle.id)
        else:
            messages.error(request,'Error while updating vehicle: '+str(form.errors))

    context = {'vehicle':vehicle, 'form':form}
    return render(request, 'user/vehicle/edit-vehicle.html', context)

@login_required(login_url='index')
@fleet_manager_only
def deleteVehicle(request, pk):
    fleet_manager = FleetManager.objects.get(user=request.user)
    vehicle = get_object_or_404(Vehicle, id=pk, company=fleet_manager.company)
    
    if request.method == 'POST':
        messages.success(request,'The vehicle %s was deleted successfuly' % vehicle)
        Vehicle.delete(vehicle)
        return redirect('home')

    context = {'vehicle':vehicle}
    return render(request, 'user/vehicle/delete-vehicle.html', context)

@login_required(login_url='index')
@fleet_manager_only
def editTiresVehicle(request, pk):
    fleet_manager = FleetManager.objects.get(user=request.user)
    vehicle = get_object_or_404(Vehicle, id=pk, company=fleet_manager.company)
    form = VehicleFormOnlyTires(instance=vehicle, company=fleet_manager.company)
    
    if request.method == 'POST':
        form = VehicleFormOnlyTires(request.POST, instance=vehicle, company=fleet_manager.company)

        if form.is_valid():
            form.save()
            v = get_object_or_404(Vehicle, id=pk)
            v.setAllTiresUsed
            v.save()
            messages.success(request,'The vehicle %s\'s tires are been update successfuly' % vehicle)
            return redirect('vehicle', vehicle.id)
        else:
            messages.error(request,'Error while updating vehicle: '+str(form.errors))

    context = {'vehicle':vehicle, 'form':form}
    return render(request, 'user/vehicle/edit-tires-vehicle.html', context)

@login_required(login_url='index')
@fleet_manager_only
def tire(request, pk):
    fleet_manager = FleetManager.objects.get(user=request.user)
    tire = get_object_or_404(Tire, id=pk, company=fleet_manager.company)

    context = {'tire':tire}
    return render(request, 'user/tire/tire.html', context)

@login_required(login_url='index')
@fleet_manager_only
def addTire(request):
    fleet_manager = FleetManager.objects.get(user=request.user)
    form = TireForm(company=fleet_manager.company)

    if request.method == 'POST':
        form = TireForm(request.POST, company=fleet_manager.company)
        if form.is_valid():
            new_tire = form.save(commit=False)
            new_tire.company = fleet_manager.company
            new_tire.save()
            form.save_m2m()
            messages.success(request,'The new tire was addedd successfuly')
            return redirect('home')

    context = {'form':form}
    return render(request, 'user/tire/add-tire.html', context)

@login_required(login_url='index')
@fleet_manager_only
def editTire(request, pk):
    fleet_manager = FleetManager.objects.get(user=request.user)
    tire = get_object_or_404(Tire, id=pk, company=fleet_manager.company)
    form = TireForm(instance=tire, company=fleet_manager.company)
    
    if request.method == 'POST':
        form = TireForm(request.POST, instance=tire, company=fleet_manager.company)

        if form.is_valid():
            form.save()
            messages.success(request,'The tire %s is been update successfuly' % tire)
            return redirect('tire', tire.id)
        else:
            messages.error(request,'Error while updating tire: '+str(form.errors))

    
    context = {'tire':tire, 'form':form}
    return render(request, 'user/tire/edit-tire.html', context)

@login_required(login_url='index')
@fleet_manager_only
def deleteTire(request, pk):
    fleet_manager = FleetManager.objects.get(user=request.user)
    tire = get_object_or_404(Tire, id=pk, company=fleet_manager.company)
    
    if request.method == 'POST':
        messages.success(request,'The tire %s was deleted successfuly' % tire)
        Tire.delete(tire)
        return redirect('home')

    context = {'tire':tire}
    return render(request, 'user/tire/delete-tire.html', context)

@login_required(login_url='index')
@fleet_manager_only
def sensor(request, pk):
    fleet_manager = FleetManager.objects.get(user=request.user)
    sensor = get_object_or_404(Sensor, id=pk, company=fleet_manager.company)

    context = {'sensor':sensor}
    return render(request, 'user/sensor/sensor.html', context)

@login_required(login_url='index')
@fleet_manager_only
def addSensor(request):
    fleet_manager = FleetManager.objects.get(user=request.user)
    form = SensorForm()

    if request.method == 'POST':
        form = SensorForm(request.POST)
        if form.is_valid():
            new_sensor = form.save(commit=False)
            new_sensor.company = fleet_manager.company
            new_sensor.save()
            form.save_m2m()
            messages.success(request,'The new sensor was addedd successfuly')
            return redirect('home')
        else:
            print(form.errors)

    context = {'form':form}
    return render(request, 'user/sensor/add-sensor.html', context)

@login_required(login_url='index')
@fleet_manager_only
def editSensor(request, pk):
    fleet_manager = FleetManager.objects.get(user=request.user)
    sensor = get_object_or_404(Sensor, id=pk, company=fleet_manager.company)
    form = SensorForm(instance=sensor)
    
    if request.method == 'POST':
        form = SensorForm(request.POST, instance=sensor)

        if form.is_valid():
            form.save()
            messages.success(request,'The sensor %s is been update successfuly' % sensor)
            return redirect('sensor', sensor.id)
        else:
            messages.error(request,'Error while updating sensor: '+str(form.errors))

    
    context = {'sensor':sensor, 'form':form}
    return render(request, 'user/sensor/edit-sensor.html', context)

@login_required(login_url='index')
@fleet_manager_only
def deleteSensor(request, pk):
    fleet_manager = FleetManager.objects.get(user=request.user)
    sensor = get_object_or_404(Sensor, id=pk, company=fleet_manager.company)
    
    if request.method == 'POST':
        messages.success(request,'The sensor %s was deleted successfuly' % sensor)
        Sensor.delete(sensor)
        return redirect('home')

    context = {'sensor':sensor}
    return render(request, 'user/sensor/delete-sensor.html', context)

def page_404(request, exception):
    return render(request, 'error/404.html')
