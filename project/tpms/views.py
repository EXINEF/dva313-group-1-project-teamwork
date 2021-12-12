from django.shortcuts import get_object_or_404, redirect, render
from django.contrib.auth.forms import AuthenticationForm
from django.contrib.auth import authenticate, login, logout
from .models import FleetManager, Vehicle, Tire, Sensor
from .forms import VehicleForm, TireForm, SensorForm
from .decorators import unauthenticated_user, company_administrator_only, allowed_users
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
            return redirect('admin-page')
        else:
            messages.error(request, 'Username OR password is incorrect')

    context = {}
    return render(request,'index/index.html', context)

@login_required(login_url='index')
def logoutPage(request):
    logout(request)
    return redirect('index')

@login_required(login_url='index')
@allowed_users(allowed_roles=['fleet-manager'])
def homePage(request):
    fleet_manager = FleetManager.objects.get(user=request.user)
    vehicles = Vehicle.objects.filter(company=fleet_manager.company)
    tires_num = Tire.objects.count()
    sensor_num = Sensor.objects.count()


    context = {'vehicles':vehicles, 'fleet_manager':fleet_manager, 'tires_num':tires_num, 'sensor_num':sensor_num}
    return render(request, 'user/home-simple.html', context)

@login_required(login_url='index')
@allowed_users(allowed_roles=['fleet-manager'])
def homePageExtended(request):
    fleet_manager = FleetManager.objects.get(user=request.user)
    vehicles = Vehicle.objects.filter(company=fleet_manager.company)

    context = {'vehicles':vehicles}
    return render(request, 'user/home-extended.html', context)

def vehicle(request, pk):
    fleet_manager = FleetManager.objects.get(user=request.user)
    vehicle = get_object_or_404(Vehicle, id=pk, company=fleet_manager.company)
    tires = vehicle.tires.all()
    location = vehicle.locations.all().order_by('-creation_datetime').first()

    context = {'vehicle':vehicle, 'tires':tires, 'location':location}
    return render(request, 'user/vehicle/vehicle.html', context)

def addVehicle(request):
    fleet_manager = FleetManager.objects.get(user=request.user)
    form = VehicleForm(initial={'company':fleet_manager.company})
    
    if request.method == 'POST':
        form = VehicleForm(request.POST)
        if form.is_valid():
            form.save()
            messages.success(request,'The new vehicle was addedd successfuly')
            return redirect('home')

    context = {'form':form}
    return render(request, 'user/vehicle/add-vehicle.html', context)

def editVehicle(request, pk):
    fleet_manager = FleetManager.objects.get(user=request.user)
    vehicle = get_object_or_404(Vehicle, id=pk, company=fleet_manager.company)
    form = VehicleForm(instance=vehicle)
    
    if request.method == 'POST':
        form = VehicleForm(request.POST, instance=vehicle)

        if form.is_valid():
            form.save()
            messages.success(request,'The vehicle %s is been update successfuly' % vehicle)
            return redirect('home')
        else:
            messages.error(request,'Error while updating vehicle: '+str(form.errors))

    
    context = {'vehicle':vehicle, 'form':form}
    return render(request, 'user/vehicle/edit-vehicle.html', context)

def deleteVehicle(request, pk):
    fleet_manager = FleetManager.objects.get(user=request.user)
    vehicle = get_object_or_404(Vehicle, id=pk, company=fleet_manager.company)
    
    if request.method == 'POST':
        messages.success(request,'The vehicle %s was deleted successfuly' % vehicle)
        Vehicle.delete(vehicle)
        return redirect('home')

    context = {'vehicle':vehicle}
    return render(request, 'user/vehicle/delete-vehicle.html', context)

def tire(request, pk):
    fleet_manager = FleetManager.objects.get(user=request.user)
    tire = get_object_or_404(Tire, id=pk, company=fleet_manager.company)

    context = {'tire':tire}
    return render(request, 'user/tire/tire.html', context)

def addTire(request):
    fleet_manager = FleetManager.objects.get(user=request.user)
    form = TireForm(initial={'company':fleet_manager.company})

    if request.method == 'POST':
        form = TireForm(request.POST)
        if form.is_valid():
            form.save()
            messages.success(request,'The new tire was addedd successfuly')
            return redirect('home')

    context = {'form':form}
    return render(request, 'user/tire/add-tire.html', context)

def editTire(request, pk):
    fleet_manager = FleetManager.objects.get(user=request.user)
    tire = get_object_or_404(Tire, id=pk, company=fleet_manager.company)
    form = TireForm(instance=tire)
    
    if request.method == 'POST':
        form = TireForm(request.POST, instance=tire)

        if form.is_valid():
            form.save()
            messages.success(request,'The tire %s is been update successfuly' % tire)
            return redirect('home')
        else:
            messages.error(request,'Error while updating tire: '+str(form.errors))

    
    context = {'tire':tire, 'form':form}
    return render(request, 'user/tire/edit-tire.html', context)

def deleteTire(request, pk):
    fleet_manager = FleetManager.objects.get(user=request.user)
    tire = get_object_or_404(Tire, id=pk, company=fleet_manager.company)
    
    if request.method == 'POST':
        messages.success(request,'The tire %s was deleted successfuly' % tire)
        Tire.delete(tire)
        return redirect('home')

    context = {'tire':tire}
    return render(request, 'user/tire/delete-tire.html', context)

def sensor(request, pk):
    fleet_manager = FleetManager.objects.get(user=request.user)
    sensor = get_object_or_404(Sensor, id=pk, company=fleet_manager.company)

    context = {'sensor':sensor}
    return render(request, 'user/sensor/sensor.html', context)

def addSensor(request):
    fleet_manager = FleetManager.objects.get(user=request.user)
    form = SensorForm(initial={'company':fleet_manager.company})

    if request.method == 'POST':
        form = SensorForm(request.POST)
        if form.is_valid():
            form.save()
            messages.success(request,'The new sensor was addedd successfuly')
            return redirect('home')

    context = {'form':form, 'form':form}
    return render(request, 'user/sensor/add-sensor.html', context)

def editSensor(request, pk):
    fleet_manager = FleetManager.objects.get(user=request.user)
    sensor = get_object_or_404(Sensor, id=pk, company=fleet_manager.company)
    form = SensorForm(instance=sensor)
    
    if request.method == 'POST':
        form = SensorForm(request.POST, instance=sensor)

        if form.is_valid():
            form.save()
            messages.success(request,'The sensor %s is been update successfuly' % sensor)
            return redirect('home')
        else:
            messages.error(request,'Error while updating sensor: '+str(form.errors))

    
    context = {'sensor':sensor, 'form':form}
    return render(request, 'user/sensor/edit-sensor.html', context)

def deleteSensor(request, pk):
    fleet_manager = FleetManager.objects.get(user=request.user)
    sensor = get_object_or_404(Sensor, id=pk, company=fleet_manager.company)
    
    if request.method == 'POST':
        messages.success(request,'The sensor %s was deleted successfuly' % sensor)
        Sensor.delete(sensor)
        return redirect('home')

    context = {'sensor':sensor}
    return render(request, 'user/sensor/delete-sensor.html', context)

# http://127.0.0.1:8000/savedata/AUTHSYSTEM-TEST111-SENSORTEST1-3234-223-BROKEN-SENSORTEST2-40234-222-WORKING-SENSORTEST3-2237-32345-DANGER-SENSORTEST4-5453-1354-WARNING
def saveData(request, query):
    chunks = query.split('-')
    if chunks[0] == 'AUTHSYSTEM':
        vehicle = get_object_or_404(Vehicle,id=chunks[1])
        tires = vehicle.tires.all()
        counter = 0
        for x in range(2, len(chunks), 4):
            sensor = tires[counter].sensor
            sensor.temperature = chunks[x+1]
            sensor.pressure = chunks[x+2]
            sensor.status = chunks[x+3]
            sensor.save()
            counter+=1
            
    return redirect('/')
